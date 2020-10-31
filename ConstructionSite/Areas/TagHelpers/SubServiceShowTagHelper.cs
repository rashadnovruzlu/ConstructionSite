using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;


namespace ConstructionSite.Areas.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "current-id")]
    public class SubServiceShowTagHelper : TagHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        string _lang;
        private IHttpContextAccessor _httpContextAccessor;
        public SubServiceShowTagHelper(IUnitOfWork unitOfWork,
                                       IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.GetLanguages();
        }

        public int CurrentId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var result = _unitOfWork.SubServiceRepository.GetAll()
                .Where(x => x.ServiceId == CurrentId)
                .Select(x => x.FindName(_lang))
                .ToList();
            TagBuilder Li = new TagBuilder("li");
           
            foreach (var item in result)
            {

                Li.InnerHtml.AppendHtml(item + "<br/>");

            }

            output.PreContent.AppendHtml(Li);




        }
    }
}
