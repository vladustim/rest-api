using PostService.CommonTypes;

namespace PostService.App;

internal static class DeliveryTypeLabels
{
    public static string GetLabel(DeliveryType type) => type switch
    {
        DeliveryType.Department => "Відділення",
        DeliveryType.Courier => "Кур’єр",
        DeliveryType.ExpressCourier => "Експрес-кур’єр",
        _ => $"Невідомий тип ({(int)type})"
    };
}
