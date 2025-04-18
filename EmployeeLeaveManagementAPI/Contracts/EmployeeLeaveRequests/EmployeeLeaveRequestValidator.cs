
namespace EmployeeLeaveManagementAPI.Contracts.Employees
{
    public class EmployeeLeaveRequestValidator: AbstractValidator<EmployeeLeaveRequest>
    {

        public EmployeeLeaveRequestValidator()
        {
            RuleFor(l => l.EmployeeId).NotEmpty();

            RuleFor(l => l.EndDate).NotEmpty().GreaterThanOrEqualTo(l => l.StartDate).GreaterThan(DateTime.Today).WithMessage("End Date Must be Greater than Today");

            RuleFor(l => l.StartDate).NotEmpty().LessThanOrEqualTo(l => l.EndDate).GreaterThan(DateTime.Today).WithMessage("Start Date Must be Greater than Today");

     

            RuleFor(l => l.Reason).NotEmpty().MaximumLength(200);

        }


    }
    
}
