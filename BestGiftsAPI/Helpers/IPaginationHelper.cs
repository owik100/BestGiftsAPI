namespace BestGiftsAPI.Helpers
{
    public interface IPaginationHelper
    {
        int CalculateTotalPages(int count, int pageSze);
    }
}