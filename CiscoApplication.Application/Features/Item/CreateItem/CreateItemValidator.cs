using FluentValidation;

namespace CiscoApplication.Application.Features.Item.CreateItem
{
    public class CreateItemValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemValidator()
        {
            ValidateBand();
            ValidateCategoryCode();
            ValidateManufacturer();
            ValidateItemDescription();
            ValidateListPrice();
            ValidateMinimumDiscount();
        }
        public void ValidateBand()
        {
            RuleFor(x => x.CreateItemDto.Band)
                .NotEmpty()
                .WithMessage("Band is required")
                .NotNull()
                .WithMessage("Band is required")
                .GreaterThan(0)
                .WithMessage("Band should be greater than 0");
        }
        public void ValidateCategoryCode()
        {
            RuleFor(x => x.CreateItemDto.CategoryCode)
                .NotEmpty()
                .WithMessage("Category Code is required")
                .NotNull()
                .WithMessage("Category Code is required")
                .MaximumLength(50)
                .WithMessage("Category Code should be less than 50 characters");
        }
        public void ValidateManufacturer()
        {
            RuleFor(x => x.CreateItemDto.Manufacturer)
                .NotEmpty()
                .WithMessage("Manufacturer is required")
                .NotNull()
                .WithMessage("Manufacturer is required")
                .MaximumLength(50)
                .WithMessage("Manufacturer Code should be less than 50 characters");
        }
        public void ValidatePartSKU()
        {
            RuleFor(x => x.CreateItemDto.PartSKU)
                .NotEmpty()
                .WithMessage("PartSKU is required")
                .NotNull()
                .WithMessage("PartSKU is required")
                .MaximumLength(50)
                .WithMessage("PartSKU Code should be less than 50 characters");
        }

        public void ValidateItemDescription()
        {
            RuleFor(x => x.CreateItemDto.ItemDescription)
                .NotEmpty()
                .WithMessage("ItemDescription is required")
                .NotNull()
                .WithMessage("ItemDescription is required")
                .MaximumLength(150)
                .WithMessage("ItemDescription Code should be less than 150 characters");
        }

        [Obsolete]
        public void ValidateListPrice()
        {
            RuleFor(x => x.CreateItemDto.ListPrice)
                .NotEmpty()
                .WithMessage("ListPrice is required")
                .NotNull()
                .WithMessage("ListPrice is required")
                .GreaterThan(0)
                .WithMessage("ListPrice should be greater than 0")
                .ScalePrecision(2, 2)
                .WithMessage("ListPrice should have 2 decimal places");
        }
        public void ValidateMinimumDiscount()
        {
            RuleFor(x => x.CreateItemDto.MinimumDiscount)
                .NotEmpty()
                .WithMessage("ListPrice is required")
                .NotNull()
                .WithMessage("ListPrice is required")
                .GreaterThan(0)
                .WithMessage("ListPrice should be greater than 0")
                .LessThan(1)
                .WithMessage("ListPrice should be less than 1");

        }
    }
}
