//
namespace OnlinePharmacy.Mappers.Generic
{
    // complete
    public interface IMapper<T1, T2>
    {
        T2 ToDTO(T1 obj);
        T1 ToObject(T2 dto);
    }
}
