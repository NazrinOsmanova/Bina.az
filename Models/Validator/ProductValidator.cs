using FluentValidation;

namespace Proje1.Models.Validator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductArea).NotEmpty().WithMessage("Evin sahesi bos ola bilmez");
            RuleFor(x => x.ProductSeherId).NotEmpty().WithMessage("Seher secin");
            RuleFor(x => x.ProductRayonId).NotEmpty().WithMessage("Rayon secin");
            RuleFor(x => x.ProductPrice).NotEmpty().WithMessage("Evin qiymeti yazilmayib");
            RuleFor(x => x.ProductMenzilId).NotEmpty().WithMessage("Elanin novunu secin");
            RuleFor(x => x.ProductFloor).NotEmpty().WithMessage("Mertebeni qeyd edin");
            RuleFor(x => x.ProductArea).NotEmpty().WithMessage("Saheni qeyd edin");
        }
    }
}
