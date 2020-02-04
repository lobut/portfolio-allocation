namespace Refinitiv.PortfolioAllocation.Domain 
{ 
    public class Security
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int QuantityHeld { get; set; }

        public int TotalPrice()
        {
            return Price * QuantityHeld;
        }
    }        
}
