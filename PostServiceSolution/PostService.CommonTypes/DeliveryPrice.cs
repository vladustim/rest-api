namespace PostService.CommonTypes;

public static class DeliveryPrice
{
    public static float GetBillableWeight(float actualWeight, int length, int width, int height)
    {
        double volumetricWeight = (double)length * width * height / 4000;
        return Math.Max(actualWeight, (float)volumetricWeight);
    }

    public static float Calculate(float billableWeight, DeliveryType deliveryType)
    {
        float pricePerKg = deliveryType switch
        {
            DeliveryType.Courier => 20f,
            DeliveryType.ExpressCourier => 30f,
            _ => 10f
        };

        return billableWeight * pricePerKg;
    }
}
