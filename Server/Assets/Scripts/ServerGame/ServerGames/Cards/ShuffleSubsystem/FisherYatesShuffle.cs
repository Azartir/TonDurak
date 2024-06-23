using System.Collections;
using System.Collections.Generic;

namespace TonDurakServer.Game
{
    public class FisherYatesShuffle : IShuffleStrategy
    {
        private System.Random _random = new System.Random();
        private List<Card> _list;

        public void Shuffle(Stack<Card> stack)
        {
            int n = stack.Count;
            _list = new List<Card>(stack);
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                Card value = _list[k];
                _list[k] = _list[n];
                _list[n] = value;
            }

            stack.Clear();

            foreach (var item in _list)
                stack.Push(item);
        }
    }

    public interface IShuffleStrategy
    {
        void Shuffle(Stack<Card> stack);
    }
}