namespace RGC.WMS.USA.Domain
{
    public class DominBaseConfig
    {
        public string SystemName { get; set; }

        public string FileUploadAddress { get; set; } //文件保存物理根目录,无法直接访问

        public string SystemRootAddress { get; set; } //图片保存物理根目录

        public string ImgSiteRootAddress { get; set; } //图片外网访问路径
    }
}
