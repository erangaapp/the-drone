using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneAPI.Models.ModelValidators
{
    public class LoadDroneWithMedicationsModelValidator : AbstractValidator<LoadDroneWithMedicationsModel>
    {
        public LoadDroneWithMedicationsModelValidator()
        {
            RuleFor(x => x.MedicationIds)
                .NotEmpty()
                .WithMessage(m => string.Format(ModelTexts.Validation_Required, nameof(m.MedicationIds)));
        }
    }
}
