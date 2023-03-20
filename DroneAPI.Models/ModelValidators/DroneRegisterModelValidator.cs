using FluentValidation;

namespace DroneAPI.Models
{
    /// <summary>
    /// Drone Register Model Validator
    /// </summary>
    /// <seealso cref="AbstractValidator&lt;DroneRegisterModel&gt;" />
    public class DroneRegisterModelValidator : AbstractValidator<DroneRegisterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneRegisterModelValidator"/> class.
        /// </summary>
        public DroneRegisterModelValidator()
        {
            RuleFor(x => x.SerialNumber)
                .NotEmpty().NotNull()
                .WithMessage(m => string.Format(ModelTexts.Validation_Required,nameof(m.SerialNumber)))
                .MaximumLength(100)
                .WithMessage(string.Format(ModelTexts.Validation_MaxLengthExceeded,"100")); 
            
            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage(m => string.Format(ModelTexts.Validation_Required,nameof(m.SerialNumber)))
                .Must((model, value, context) =>
                {
                    Core.DroneModel droneModel;
                    var result = Enum.TryParse<Core.DroneModel>(value, out droneModel);
                    bool isDefinedResult = Enum.IsDefined(typeof(Core.DroneModel), (int)droneModel);
                    return isDefinedResult;
                }).
                WithMessage(ModelTexts.Validation_DroneModel);

            RuleFor(x => x.WeightLimit)
                .NotEmpty()
                .WithMessage(m => string.Format(ModelTexts.Validation_Required, nameof(m.WeightLimit)))
                .InclusiveBetween(0,500)
                .WithMessage(string.Format(ModelTexts.Validation_MaxValueExceeded, "500"));
        }
    }
}
