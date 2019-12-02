using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackWebsite.Data;
using FeedbackWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FeedbackWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Text.RegularExpressions;

namespace FeedbackWebsite.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly FeedbackWebsiteContext _context;
        private readonly UserManager<User> _userManager;

        public FeedbackController(FeedbackWebsiteContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Feedback
        [Authorize(Roles = "admin, user")]
        public IActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("AdminIndex");
            }

            return RedirectToAction("UserIndex");
        }


        [Authorize(Roles = "user")]
        public async Task<IActionResult> UserIndex()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return NotFound();
            }
            var userEvents = _context.UserEventModels.Where(model => model.UserId == user.Id);

            List<EventInfoModel> eventInfoModels = new List<EventInfoModel>();

            foreach (var userEvent in userEvents)
            {
                var eventInfo = await _context.EventInfoModel.FirstOrDefaultAsync(m => m.Id == userEvent.EventId);
                eventInfoModels.Add(eventInfo);
            }

            var userInfoEventViewModel = new UserInfoEventViewModel()
            {
                EmployeeName = user.EmployeeName,
                Department = user.Department,
                Position = user.Position,
                Event = eventInfoModels
            };

            return View(userInfoEventViewModel);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AdminIndex()
        {
            List<UserEventDetailsViewModel> userEventDetails = new List<UserEventDetailsViewModel>();

            var userEvents = await _context.UserEventModels.ToListAsync();

            foreach (var userEvent in userEvents)
            {
                var eventInfo = await _context.EventInfoModel.FindAsync(userEvent.EventId);
                var user = await _userManager.FindByIdAsync(userEvent.UserId);

                var userEventDetail = new UserEventDetailsViewModel()
                {
                    EmployeeName = user.EmployeeName,
                    Department = user.Department,
                    Position = user.Position,
                    EventId = eventInfo.Id,
                    EventName = eventInfo.EventName,
                    EventOrg = eventInfo.EventOrg,
                    PresentersName = eventInfo.PresentersName,
                    EventLocation = eventInfo.EventLocation,
                    EventStartDate = eventInfo.EventStartDate,
                    EventEndDate = eventInfo.EventEndDate
                };

                userEventDetails.Add(userEventDetail);
            }

            return View(userEventDetails);
        }

        //// GET: Feedback/Details/5
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEvent = await _context.UserEventModels.FirstOrDefaultAsync(model => model.EventId == id);

            if (userEvent == null)
            {
                return NotFound();
            }

            var eventInfo = await _context.EventInfoModel.FirstOrDefaultAsync(m => m.Id == userEvent.EventId);

            if (eventInfo == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userEvent.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var userEventDetails = new UserEventDetailsViewModel()
            {
                EmployeeName = user.EmployeeName,
                Department = user.Department,
                Position = user.Position,
                EventId = eventInfo.Id,
                EventName = eventInfo.EventName,
                EventOrg = eventInfo.EventOrg,
                PresentersName = eventInfo.PresentersName,
                EventLocation = eventInfo.EventLocation,
                EventStartDate = eventInfo.EventStartDate,
                EventEndDate = eventInfo.EventEndDate
            };

            return View(userEventDetails);
            //return View(eventInfoModel);
        }

        // GET: Feedback/Create
        [Authorize(Roles = "admin, user")]
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Feedback/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Create([Bind("Id,EventName,EventOrg,PresentersName,EventLocation,EventStartDate,EventEndDate")] EventInfoModel eventInfoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventInfoModel);
                await _context.SaveChangesAsync();

                var user = await _userManager.GetUserAsync(HttpContext.User);
                
                var userEvent = new UserEventModel()
                {
                    UserId = user.Id,
                    EventId = eventInfoModel.Id
                };

                _context.Add(userEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddOrEdit", "Questions", new { eventId = eventInfoModel.Id });
                //return RedirectToAction("Index");
            }
            return View(eventInfoModel);
        }

        //// GET: Feedback/Edit/5
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventInfoModel = await _context.EventInfoModel.FindAsync(id);
            if (eventInfoModel == null)
            {
                return NotFound();
            }
            return View(eventInfoModel);
        }

        //// POST: Feedback/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventName,EventOrg,PresentersName,EventLocation,EventStartDate,EventEndDate")] EventInfoModel eventInfoModel)
        {
            if (id != eventInfoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventInfoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventInfoModelExists(eventInfoModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("AddOrEdit", "Questions", new { eventId = eventInfoModel.Id });
                //return RedirectToAction("Index");
            }
            return View(eventInfoModel);
        }

        //// GET: Feedback/Delete/5
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEvent = await _context.UserEventModels.FirstOrDefaultAsync(model => model.EventId == id);

            if (userEvent == null)
            {
                return NotFound();
            }

            var eventInfo = await _context.EventInfoModel.FirstOrDefaultAsync(m => m.Id == userEvent.EventId);

            if (eventInfo == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userEvent.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var userEventDetails = new UserEventDetailsViewModel()
            {
                EmployeeName = user.EmployeeName,
                Department = user.Department,
                Position = user.Position,
                EventId = eventInfo.Id,
                EventName = eventInfo.EventName,
                EventOrg = eventInfo.EventOrg,
                PresentersName = eventInfo.PresentersName,
                EventLocation = eventInfo.EventLocation,
                EventStartDate = eventInfo.EventStartDate,
                EventEndDate = eventInfo.EventEndDate
            };

            return View(userEventDetails);
        }

        // POST: Feedback/Delete/5
        [Authorize(Roles = "admin, user")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int eventId)
        {
            var eventInfoModel = await _context.EventInfoModel.FindAsync(eventId);
            _context.EventInfoModel.Remove(eventInfoModel);
            await _context.SaveChangesAsync();

            var userEvent = await _context.UserEventModels.FirstOrDefaultAsync(m => m.EventId == eventId);
            _context.UserEventModels.Remove(userEvent);

            var answersInfoModels = _context.AnswersInfoModel.Where(model => model.EventId == eventId);

            foreach (var answerInfoModel in answersInfoModels)
            {
                _context.AnswersInfoModel.Remove(answerInfoModel);
            }

            await _context.SaveChangesAsync();

            //return RedirectToAction("Delete", "Questions", new { eventId = eventInfoModel.Id });
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> DownloadExcel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEvent = await _context.UserEventModels.FirstOrDefaultAsync(model => model.EventId == id);

            if (userEvent == null)
            {
                return NotFound();
            }

            var eventInfo = await _context.EventInfoModel.FirstOrDefaultAsync(m => m.Id == userEvent.EventId);

            if (eventInfo == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userEvent.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var userEventDetails = new UserEventDetailsViewModel()
            {
                EmployeeName = user.EmployeeName,
                Department = user.Department,
                Position = user.Position,
                EventId = eventInfo.Id,
                EventName = eventInfo.EventName,
                EventOrg = eventInfo.EventOrg,
                PresentersName = eventInfo.PresentersName,
                EventLocation = eventInfo.EventLocation,
                EventStartDate = eventInfo.EventStartDate,
                EventEndDate = eventInfo.EventEndDate
            };

            //string fileName = "Event Feedback Form.xlsx";
            //string sourcePath = @"D:\Core\FeedbackService\FeedbackWebsite\FeedbackWebsite\Excel\Source"; 

            string fileName = @"Excel\Source\Event Feedback Form.xlsx";

            var exePath = Path.GetDirectoryName(System.Reflection
                .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;

            //string soutceFile = Path.Combine(sourcePath, fileName);
            string soutceFile = Path.Combine(appRoot, fileName);

            MemoryStream ms;
            using (var fs = new FileStream(soutceFile, FileMode.Open, FileAccess.Read))
            {
                ms = new MemoryStream();
                fs.CopyTo(ms);
                ms.Position = 0;
            }
           

            UpdateCell(ms, userEventDetails.EmployeeName, 4, "B");

            string buf = $"{userEventDetails.Department} \\ {userEventDetails.Position}";
            UpdateCell(ms, buf, 5, "B");

            UpdateCell(ms, userEventDetails.EventName, 6, "B");

            UpdateCell(ms, userEventDetails.EventOrg, 7, "B");

            UpdateCell(ms, userEventDetails.PresentersName, 8, "B");

            UpdateCell(ms, userEventDetails.EventLocation, 9, "B");

            UpdateCell(ms, userEventDetails.EventStartDate.Day, 10, "B");
            UpdateCell(ms, userEventDetails.EventStartDate.Month, 10, "C");
            UpdateCell(ms, userEventDetails.EventStartDate.Year, 10, "D");

            UpdateCell(ms, userEventDetails.EventEndDate.Day, 11, "B");
            UpdateCell(ms, userEventDetails.EventEndDate.Month, 11, "C");
            UpdateCell(ms, userEventDetails.EventEndDate.Year, 11, "D");

            var answersInfoEnums = _context.AnswerEnum.Where(model => model.EventId == id);

            uint counter = 14;
            foreach (var answersInfoEnum in answersInfoEnums)
            {
                buf = ReturnStringAnswer(answersInfoEnum.QuestionEnumAnswer);
                UpdateCell(ms, buf, counter, "E");
                counter++;
            }

            AnswerText answerText = await _context.AnswerText.FirstOrDefaultAsync(model => model.EventId == id);
            UpdateCell(ms, answerText.AnswerTextAnswer, 25, "A");
            ms.Position = 0;


            string downloadFileName = $"{userEventDetails.EmployeeName} - {userEventDetails.EventName} - {fileName}";
            return File(ms, "application/xlsx", downloadFileName);
            //return RedirectToAction("Index");
        }

        public static string ReturnStringAnswer(Answer answer)
        {
            string buf = null;
            switch ((int)answer)
            {
                case 0:
                    buf = "(1) Strongly Disagree";
                    break;
                case 1:
                    buf = "(2) Disagree";
                    break;
                case 2:
                    buf = "(3) Neither Agree nor Disagree";
                    break;
                case 3:
                    buf = "(4) Agree";
                    break;
                case 4:
                    buf = "(5) Strongly Agree";
                    break;
            }

            return buf;
        }

        public static void UpdateCell(MemoryStream memoryStream, string text,
            uint rowIndex, string columnName)
        {
            // Open the document for editing.
            using (SpreadsheetDocument spreadSheet =
                SpreadsheetDocument.Open(memoryStream, true))
            {
                WorksheetPart worksheetPart =
                    GetWorksheetPartByName(spreadSheet, "EFF");

                if (worksheetPart != null)
                {
                    Cell cell = GetCell(worksheetPart.Worksheet,
                        columnName, rowIndex);

                    cell.CellValue = new CellValue(text);
                    cell.DataType =
                        new EnumValue<CellValues>(CellValues.String);

                    // Save the worksheet.
                    worksheetPart.Worksheet.Save();
                }
            }

        }

        public static void UpdateCell(MemoryStream memoryStream, int value,
            uint rowIndex, string columnName)
        {
            // Open the document for editing.
            using (SpreadsheetDocument spreadSheet =
                SpreadsheetDocument.Open(memoryStream, true))
            {
                WorksheetPart worksheetPart =
                    GetWorksheetPartByName(spreadSheet, "EFF");

                if (worksheetPart != null)
                {
                    Cell cell = GetCell(worksheetPart.Worksheet,
                        columnName, rowIndex);

                    cell.CellValue = new CellValue(value.ToString());
                    cell.DataType =
                        new EnumValue<CellValues>(CellValues.Number);

                    // Save the worksheet.
                    worksheetPart.Worksheet.Save();
                }
            }

        }

        private static WorksheetPart
            GetWorksheetPartByName(SpreadsheetDocument document,
                string sheetName)
        {
            IEnumerable<Sheet> sheets =
                document.WorkbookPart.Workbook.GetFirstChild<Sheets>().
                    Elements<Sheet>().Where(s => s.Name == sheetName);

            var enumerable = sheets as Sheet[] ?? sheets.ToArray();
            if (!enumerable.Any())
            {
                // The specified worksheet does not exist.

                return null;
            }

            string relationshipId = enumerable.First().Id.Value;
            WorksheetPart worksheetPart = (WorksheetPart)
                document.WorkbookPart.GetPartById(relationshipId);
            return worksheetPart;

        }

        private static Cell GetCell(Worksheet worksheet,
            string columnName, uint rowIndex)
        {
            Row row = GetRow(worksheet, rowIndex);

            if (row == null)
                return null;

            return row.Elements<Cell>().Where(c => String.Compare
                                                   (c.CellReference.Value, columnName +
                                                                           rowIndex, StringComparison.OrdinalIgnoreCase) == 0).First();
        }

        private static Row GetRow(Worksheet worksheet, uint rowIndex)
        {
            return worksheet.GetFirstChild<SheetData>().
                Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
        }


        private bool EventInfoModelExists(int id)
        {
            return _context.EventInfoModel.Any(e => e.Id == id);
        }

        //private void CopyExcelFile(string soutceFile, string destFile)
        //{
        //    System.IO.File.Copy(soutceFile, destFile, true);
        //}

        //private void DeleteExcelFile(string destFile)
        //{

        //    if (System.IO.File.Exists(destFile))
        //    {
        //        try
        //        {
        //            System.IO.File.Delete(destFile);
        //        }
        //        catch (System.IO.IOException e)
        //        {
        //            throw;
        //        }
        //    }

        //}
    }
}
