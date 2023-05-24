using BussinesLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ProjectMVC.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> Index()
        {
           var departments= await _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if(ModelState.IsValid)
            {
               await _unitOfWork.DepartmentRepository.Add(department);

               await _unitOfWork.Complete();
                 TempData["Message"] = "Department Created successfully";

                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var department = await _unitOfWork.DepartmentRepository.GetById(id.Value);

            if(department is null)
                return NotFound();
            return View(viewName,department);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id,"Edit");
            //if(id is null)
            //        return BadRequest();
            //var department = _departmentRepository.GetById(id.Value);
            //if(department is null)
            //    return NotFound();
            //return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id,Department department)
        {
            if(id != department.Id)
                return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
               _unitOfWork.DepartmentRepository.Update(department);
                   await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
            //if (id is null)
            //    return BadRequest();
            //var department = _departmentRepository.GetById(id.Value);
            //if (department is null)
            //    return NotFound();
            //return View(department);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int id,Department dept)
        {
            if(id!= dept.Id)
                return BadRequest();
            try
            {

            _unitOfWork.DepartmentRepository.Delete(dept);
              await  _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                
            }
            return View(dept);
        }
    }
}
