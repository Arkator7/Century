public class Order
{
    public int victoryPoint;
    public Caravan requiredGems;

    public Order(int vicPoint, Caravan value)
    {
        victoryPoint = vicPoint;
        requiredGems = value;
    }

    public string OrderNotation()
    {
        return this.victoryPoint + " - {" +
            this.requiredGems.CaravanNotation() + "}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj == this) return true;
        var order = obj as Order;
        return order != null && Equals(order);
    }

    protected bool Equals(Order order)
    {
        return victoryPoint.Equals(order.victoryPoint)
               && requiredGems.Equals(order.requiredGems);
    }
}
