namespace imady.WxContext
{
    //2019-08-05 Frank: WxService项目暂时未用到数据库操作，因此也不需要DbContextOption。暂时保留WxContextOption。
    public class WxContextOptions
    {
        public string ConnectionString { get; set; }
        
    }

}