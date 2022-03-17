
namespace Genpai
{
    public class ClickManager : Singleton<SummonManager>
    {
        public void CleanAllClickCache()
        {
            SummonManager.Instance.SummonCancel();
        }
    }
}
