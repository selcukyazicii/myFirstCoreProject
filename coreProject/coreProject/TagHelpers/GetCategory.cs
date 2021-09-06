using coreProject.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.TagHelpers
{
    //buraya tekrar bakılacak
    [HtmlTargetElement("getCategoryName")]
    public class GetCategory : TagHelper
    {
        private readonly IProductRepository _productRepository;
        public GetCategory(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public int ProductId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string data = " ";
            var incomingCategory = _productRepository.GetCategories(ProductId).Select(x => x.Name);
            foreach (var item in incomingCategory)
            {
                data += item + " ";
            }
            output.Content.SetContent(data);
        }
    }
}
