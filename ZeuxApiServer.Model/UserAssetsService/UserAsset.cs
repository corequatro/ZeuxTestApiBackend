namespace ZeuxApiServer.Model.UserAssetsService
{
    public class UserAsset : BaseIdentity
    {
        public string Name { get; set; }
        public ProductTypeEnum ProductType { get; set; }

        //TODO add money class https://www.codeproject.com/Articles/28244/A-Money-type-for-the-CLR
        public decimal Price { get; set; }
        public CurrencyEnum Currency { get; set; }
        public decimal Returns { get; set; }
    }
}
