namespace Dotnet9.WebAPI.Service.Domain.Aggregates.Orders;

public class AddressValue : ValueObject
{
    public string Address { get; private set; } = string.Empty;

    public string ProvinceCode { get; private set; } = string.Empty;

    public string CityCode { get; private set; } = string.Empty;

    public AddressValue()
    {
    }

    public AddressValue(string address, string provinceCode, string cityCode)
    {
        Address = address;
        ProvinceCode = provinceCode;
        CityCode = cityCode;
    }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Address;
        yield return ProvinceCode;
        yield return CityCode;
    }
}