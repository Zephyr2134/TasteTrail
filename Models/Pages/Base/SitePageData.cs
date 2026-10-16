using EPiServer.Core;
using EPiServer.SpecializedProperties;
using System.ComponentModel.DataAnnotations;

namespace TasteTrail.Models.Pages.Base
{
    public abstract class SitePageData : PageData
    {
        [CultureSpecific]
        [Display(Name="Meta Title", GroupName = "SEO", Order = 10)]
        public virtual string MetaTitle { get; set; }
        [CultureSpecific]
        [Display(Name = "Meta Description", GroupName = "SEO", Order = 20)]
        public virtual string MetaDescription { get; set; }
    }
}
