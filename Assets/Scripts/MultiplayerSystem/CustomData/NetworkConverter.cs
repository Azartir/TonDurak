using System;
using System.IO;
using CardsInventorySystem.CardsSystem;
using TableSystem;

namespace MultiplayerSystem.CustomData
{
    public static class NetworkConverter
    {
        public static NetworkCardData[] GetConvertedByteArrayToNetworkCardDataArray(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return new NetworkCardData[0];
            }

            int intSize = sizeof(int);
            int cardTypeSize = sizeof(int);
            int structSize = intSize + cardTypeSize;

            if (byteArray.Length % structSize != 0)
            {
                throw new ArgumentException("Byte array length is not aligned with the size of NetworkCardData.");
            }

            int structCount = byteArray.Length / structSize;
            NetworkCardData[] networkCardDataArray = new NetworkCardData[structCount];

            for (int i = 0; i < byteArray.Length; i += structSize)
            {
                int id = BitConverter.ToInt32(byteArray, i);
                CardType cardType = (CardType)BitConverter.ToInt32(byteArray, i + intSize);
                networkCardDataArray[i / structSize] = new NetworkCardData(id, cardType);
            }

            return networkCardDataArray;
        }

        public static byte[] GetConvertedNetworkCardDataArrayToByteArray(NetworkCardData[] networkCardDataArray)
        {
            if (networkCardDataArray == null || networkCardDataArray.Length == 0)
            {
                return new byte[0];
            }

            int intSize = sizeof(int);
            int structSize = intSize + intSize; // Id and CardType (assuming CardType is an int)

            byte[] byteArray = new byte[networkCardDataArray.Length * structSize];

            for (int i = 0; i < networkCardDataArray.Length; i++)
            {
                int id = networkCardDataArray[i].Id;
                int cardType = (int)networkCardDataArray[i].CardType;

                Array.Copy(BitConverter.GetBytes(id), 0, byteArray, i * structSize, intSize);
                Array.Copy(BitConverter.GetBytes(cardType), 0, byteArray, i * structSize + intSize, intSize);
            }

            return byteArray;
        }

        public static NetworkPlayerData[] GetConvertedByteArrayToNetworkPlayerDataArray(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return new NetworkPlayerData[0];
            }

            int structSize = sizeof(int) + sizeof(ushort);
            if (byteArray.Length % structSize != 0)
            {
                throw new ArgumentException("Byte array length is not aligned with the size of NetworkPlayerData.");
            }

            int structCount = byteArray.Length / structSize;
            NetworkPlayerData[] networkPlayerDataArray = new NetworkPlayerData[structCount];

            for (int i = 0; i < structCount; i++)
            {
                int id = BitConverter.ToInt32(byteArray, i * structSize);
                ushort queueIndex = BitConverter.ToUInt16(byteArray, i * structSize + sizeof(int));

                networkPlayerDataArray[i] = new NetworkPlayerData(id, queueIndex);
            }

            return networkPlayerDataArray;
        }

        public static byte[] GetConvertedNetworkPlayerDataArrayToByteArray(NetworkPlayerData[] networkPlayerDataArray)
        {
            if (networkPlayerDataArray == null || networkPlayerDataArray.Length == 0)
            {
                return new byte[0];
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                foreach (NetworkPlayerData playerData in networkPlayerDataArray)
                {
                    byte[] idBytes = BitConverter.GetBytes(playerData.Id);
                    byte[] queueIndexBytes = BitConverter.GetBytes(playerData.QueueIndex);

                    memoryStream.Write(idBytes, 0, idBytes.Length);
                    memoryStream.Write(queueIndexBytes, 0, queueIndexBytes.Length);
                }

                return memoryStream.ToArray();
            }
        }

        public static byte[] GetConvertedNetworkTableCardDataArrayToByteArray(NetworkTableCardData[] dataArray)
        {
            if (dataArray == null || dataArray.Length == 0)
            {
                return new byte[0];
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                foreach (var data in dataArray)
                {
                    byte[] currentCardBytes = ConvertNetworkCardDataToByteArray(data.CurrentCardData);
                    byte[] bitCardBytes = ConvertNetworkCardDataToByteArray(data.BitCardData);

                    memoryStream.Write(currentCardBytes, 0, currentCardBytes.Length);
                    memoryStream.Write(bitCardBytes, 0, bitCardBytes.Length);
                }

                return memoryStream.ToArray();
            }
        }

        private static byte[] ConvertNetworkCardDataToByteArray(NetworkCardData cardData)
        {
            byte[] idBytes = BitConverter.GetBytes(cardData.Id);
            byte[] cardTypeBytes = BitConverter.GetBytes((int)cardData.CardType);

            byte[] result = new byte[idBytes.Length + cardTypeBytes.Length];
            Array.Copy(idBytes, 0, result, 0, idBytes.Length);
            Array.Copy(cardTypeBytes, 0, result, idBytes.Length, cardTypeBytes.Length);

            return result;
        }

        public static NetworkTableCardData[] GetConvertedByteArrayToNetworkTableCardDataArray(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return new NetworkTableCardData[0];
            }

            int structSize = (sizeof(int) + sizeof(int)) * 2; // Two NetworkCardData structs
            if (byteArray.Length % structSize != 0)
            {
                throw new ArgumentException("Byte array length is not aligned with the size of NetworkTableCardData.");
            }

            int structCount = byteArray.Length / structSize;
            NetworkTableCardData[] dataArray = new NetworkTableCardData[structCount];

            for (int i = 0; i < structCount; i++)
            {
                int offset = i * structSize;
                NetworkCardData currentCard = ConvertByteArrayToNetworkCardData(byteArray, offset);
                NetworkCardData bitCard =
                    ConvertByteArrayToNetworkCardData(byteArray, offset + (sizeof(int) + sizeof(int)));

                dataArray[i] = new NetworkTableCardData(currentCard, bitCard);
            }

            return dataArray;
        }

        private static NetworkCardData ConvertByteArrayToNetworkCardData(byte[] byteArray, int offset)
        {
            int id = BitConverter.ToInt32(byteArray, offset);
            CardType cardType = (CardType)BitConverter.ToInt32(byteArray, offset + sizeof(int));

            return new NetworkCardData(id, cardType);
        }

        public static NetworkTableCardData[] AddElement(NetworkTableCardData[] originalArray,
            NetworkTableCardData newElement)
        {
            if (originalArray == null)
            {
                return new NetworkTableCardData[] { newElement };
            }

            NetworkTableCardData[] newArray = new NetworkTableCardData[originalArray.Length + 1];
            Array.Copy(originalArray, newArray, originalArray.Length);
            newArray[originalArray.Length] = newElement;

            return newArray;
        }
    }
}