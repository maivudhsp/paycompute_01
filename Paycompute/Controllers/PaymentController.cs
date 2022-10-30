using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Paycompute.Entity;
using Paycompute.Models;
using Paycomputer.service;
using System.Collections;
using System.Data;

namespace Paycompute.Controllers
{
    public class PaymentController : Controller
    {

        private readonly IPayService  _payService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaxService _taxService;
        private readonly INationalInsuranceService _nationalInsuranceService;
        private decimal overtimeHrs;
        private decimal contractualEarnings;
        private decimal overtimeEarnings;
        private decimal totalEarnings;
        private decimal tax;
        private decimal unionFee;
        private decimal studentLoan;
        private decimal nationalInsurance;
        private decimal totalDeduction;

        public PaymentController(IPayService payService, IEmployeeService employeeService, ITaxService taxService, INationalInsuranceService nationalInsuranceService)
        {
            _payService = payService;
            _employeeService = employeeService;
            _taxService = taxService;
            _nationalInsuranceService = nationalInsuranceService;
          
        }

        public IActionResult Index()
        {
            var payRecords = _payService.GetAll().Select(pay => new PaymentRecordIndexViewModel
            {
                Id = pay.Id,
                EmployeeId = pay.EmployeeId,
                FullName = pay.FullName,
                PayDate = pay.PayDate,
                PayMonth = pay.PayMonth,
                TaxYearId = pay.TaxYearId,
                Year = _payService.GetTaxYearById(pay.TaxYearId).YearOfTax,
                TotalEarnings = pay.TotalEarnings,
                TotalDeduction = pay.TotalDeduction,
                NetPayment = pay.NetPayment,
                Employee = pay.Employee
            });
            return View(payRecords);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.employees = _employeeService.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payService.GetAllTaxYear();
            var model = new PaymentRecordCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentRecordCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var payrecord = new PaymentRecord()
                {
                    Id = model.Id,
                    EmployeeId = model.EmployeeId,
                    FullName = _employeeService.GetById(model.EmployeeId).FullName,
                    NiNo = _employeeService.GetById(model.EmployeeId).NationalInsuranceNo,
                    PayDate = model.PayDate,
                    PayMonth = model.PayMonth,
                    TaxYearId = model.TaxYearId,
                    TaxCode = model.TaxCode,
                    HourlyRate = model.HourlyRate,
                    HoursWorked = model.HoursWorked,
                    ContractualHours = model.ContractualHours,
                    OvertimeHours = overtimeHrs = _payService.OvertimeHours(model.HoursWorked, model.ContractualHours),
                    ContractualEarnings = contractualEarnings = _payService.ContractualEarnings(model.ContractualHours, model.HoursWorked, model.HourlyRate),
                    OvertimeEarnings = overtimeEarnings = _payService.OvertimeEarnings(_payService.OvertimeRate(model.HourlyRate), overtimeHrs),
                    TotalEarnings = totalEarnings = _payService.TotalEarnings(overtimeEarnings, contractualEarnings),
                    Tax = tax = _taxService.TaxAmount(totalEarnings),
                    UnionFee = unionFee = _employeeService.UnionFees(model.EmployeeId),
                    SLC = studentLoan = _employeeService.StudentLoanRepaymentAmount(model.EmployeeId, totalEarnings),
                    NIC = nationalInsurance = _nationalInsuranceService.NIContribution(totalEarnings),
                    TotalDeduction = totalDeduction = _payService.TotalDeduction(tax, nationalInsurance, studentLoan, unionFee),
                    NetPayment = _payService.NetPay(totalEarnings, totalDeduction)
                };
                await _payService.CreateAsync(payrecord);
                return RedirectToAction("Index");
            }
            ViewBag.employees = _employeeService.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payService.GetAllTaxYear();
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var paymentRecord = _payService.GetById(id);
            if (paymentRecord == null)
            {
                return NotFound();
            }

            var model = new PaymentRecordDetailViewModel()
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                FullName = paymentRecord.FullName,
                NiNo = paymentRecord.NiNo,
                PayDate = paymentRecord.PayDate,
                PayMonth = paymentRecord.PayMonth,
                TaxYearId = paymentRecord.TaxYearId,
                Year = _payService.GetTaxYearById(paymentRecord.TaxYearId).YearOfTax,
                TaxCode = paymentRecord.TaxCode,
                HourlyRate = paymentRecord.HourlyRate,
                HoursWorked = paymentRecord.HoursWorked,
                ContractualHours = paymentRecord.ContractualHours,
                OvertimeHours = paymentRecord.OvertimeHours,
                OvertimeRate = _payService.OvertimeRate(paymentRecord.HourlyRate),
                ContractualEarnings = paymentRecord.ContractualEarnings,
                OvertimeEarnings = paymentRecord.OvertimeEarnings,
                Tax = paymentRecord.Tax,
                NIC = paymentRecord.NIC,
                UnionFee = paymentRecord.UnionFee,
                SLC = paymentRecord.SLC,
                TotalEarnings = paymentRecord.TotalEarnings,
                TotalDeduction = paymentRecord.TotalDeduction,
                Employee = paymentRecord.Employee,
                TaxYear = paymentRecord.TaxYear,
                NetPayment = paymentRecord.NetPayment
            };
            return View(model);
        }

    }
}
