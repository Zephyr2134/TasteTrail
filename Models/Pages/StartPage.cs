using TasteTrail.Models.Pages.Base;
using EPiServer.Core;
using EPiServer.SpecializedProperties;
using System.ComponentModel.DataAnnotations;

namespace TasteTrail.Models.Pages
{
    [ContentType(DisplayName = "Start Page", GUID = "19671657-B684-4D95-A61F-8DD4FE60D559", Description = "Taste Trail Home Page")]
    public class StartPage : SitePageData
    {
        [CultureSpecific]
        [Display(Name = "Site Title", GroupName = "Content", Order = 10)]
        public virtual string SiteTitle { get; set; }
        [CultureSpecific]
        [Display(Name = "Site Description", GroupName = "Content", Order = 20)]
        [UIHint("textarea")]
        public virtual string SiteDescription { get; set; }
    }
}
