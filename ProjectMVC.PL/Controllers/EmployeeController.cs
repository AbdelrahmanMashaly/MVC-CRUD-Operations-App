using AutoMapper;
using BussinesLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ProjectMVC.PL.Helpers;
using ProjectMVC.PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace ProjectMVC.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;

        //Adding Unit OF Work Design Pattern >>
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(/*IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository*/
            IUnitOfWork unitOfWork
            ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if(string.IsNullOrEmpty(SearchValue))
                employees = await _unitOfWork.employeeRepository.GetAll();
            else
                employees = _unitOfWork.employeeRepository.GetEmployeeByName(SearchValue);
            
            var MappedEmps= _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);
            return View(MappedEmps);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Department= await _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName = await DocumentSetting.UploadFile(employeeVM.Image, "images");
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                await _unitOfWork.employeeRepository.Add(MappedEmp);
               await  _unitOfWork.Complete();
                
                TempData["Message"] = "Employee added successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var Emp = await _unitOfWork.employeeRepository.GetById(id.Value);
           
            if (Emp is null)
                return NotFound();
            var MappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Emp);
            return View(ViewName,MappedEmp);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Department = await _unitOfWork.DepartmentRepository.GetAll();
            return await Details(id,"Edit");
            //if (id is null)
            //    return BadRequest();
            //var Emp = _employeeRepository.GetById(id.Value);
            //if (Emp is null)
            //    return NotFound();
            //return View(Emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id,EmployeeViewModel employeeVM)
        {
            if(id != employeeVM.Id)
            return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                    _unitOfWork.employeeRepository.Update(MappedEmp);
                    await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int id, EmployeeViewModel employeeVM)
        {
            if(id != employeeVM.Id)
                return BadRequest();
            try
            {
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

               _unitOfWork.employeeRepository.Delete(MappedEmp);
              int count = await _unitOfWork.Complete();
                if (count > 0)
                    DocumentSetting.DeleteImage(MappedEmp.ImageName, "images");
            return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);
            }
        }
    }
}
