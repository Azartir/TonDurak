using System.Threading.Tasks;

public interface ICardDataProvider
{
    Task<string[]> GetCardDeal(); //список карт для выдачи
    //хз как, но каждый раз вызывать новый в конце хода
}
