using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _1411031042_final.Models;
using _1411031042_final.Services;
using _1411031042_final.ViewModels;
using System.IO;

namespace _1411031042_final.Controllers
{
    public class CARController : Controller
    {
        private readonly CARDBServices CARServices = new CARDBServices();
        // GET: CAR
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Benz()
        {
            CARViewModels Data = new CARViewModels();
            Data.BenzList = CARServices.GetBenzList();
            return View(Data);
        }
        public ActionResult BMW()
        {
            CARViewModels Data = new CARViewModels();
            Data.BMWList = CARServices.GetBMWList();
            return View(Data);
        }
        public ActionResult saveBenz(Benz files)
        {
            string image = files.file.FileName;


            image = image.ToLower();
            string serverPath = Path.Combine(Server.MapPath("~/Images/"), image);
            //string serverPath = Server.MapPath("~/Images/"); // 伺服器圖片儲存位置
            files.file.SaveAs(serverPath);

            // 插入資料到資料庫
            CARDBServices dbServices = new CARDBServices();
            files.Image = image;
            dbServices.InsertBenzFile(files);
            
            return RedirectToAction("Benz");
        }
        public ActionResult saveBMW(BMW files)
        {
            string image = files.file.FileName;


            image = image.ToLower();
            string serverPath = Path.Combine(Server.MapPath("~/Images/"), image);
            //string serverPath = Server.MapPath("~/Images/"); // 伺服器圖片儲存位置
            files.file.SaveAs(serverPath);

            // 插入資料到資料庫
            CARDBServices dbServices = new CARDBServices();
            files.Image = image;
            dbServices.InsertBMWFile(files);

            return RedirectToAction("BMW");
        }
        public ActionResult DeleteBenz(int ID)
        {
            try
            {
                // 創建 CARDBServices 的實例
                CARDBServices dbServices = new CARDBServices();

                // 呼叫 DeleteBenz 方法進行資料刪除
                dbServices.DeleteBenz(ID);

                TempData["message"] = "資料已成功刪除";
            }
            catch (Exception ex)
            {
                TempData["error"] = "刪除 Benz 資料時發生錯誤：" + ex.Message;
            }

            // 重新導向到 Benz 頁面
            return RedirectToAction("Benz");
        }

        public ActionResult DeleteBMW(int ID)
        {
            try
            {
                // 建立 CARDBServices 類別的實例
                CARDBServices dbServices = new CARDBServices();

                // 呼叫 DeleteBMW 方法進行資料刪除
                dbServices.DeleteBMW(ID);

                TempData["message"] = "資料已成功刪除";
            }
            catch (Exception ex)
            {
                TempData["error"] = "刪除 BMW 資料時發生錯誤：" + ex.Message;
            }

            // 重新導向到 BMW 頁面
            return RedirectToAction("BMW");
        }
        public ActionResult EditBenz(int id)
        {
            // 根據 id 從資料庫獲取 Benz 資料
            CARDBServices dbServices = new CARDBServices();
            Benz benz = dbServices.GetBenzByID(id);

            // 將 Benz 資料映射到 BenzViewModel
            BenzViewModel viewModel = new BenzViewModel
            {
                ID = benz.ID,
                Image = benz.Image,
                Model = benz.Model,
                Money = benz.Money
            };

            return View(viewModel);
        }

        // POST: SaveInfoBenz
        [HttpPost]
        public ActionResult SaveInfoBenz(int id, BenzViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // 檢查圖片是否有變更
                if (viewModel.NewImage != null && viewModel.NewImage.ContentLength > 0)
                {
                    // 上傳新圖片並獲取保存的路徑
                    string serverPath = Server.MapPath("~/Images/");
                    string imageFileName = Path.GetFileName(viewModel.NewImage.FileName);
                    string saveUrl = serverPath + imageFileName;
                    viewModel.NewImage.SaveAs(saveUrl);

                    // 更新 Benz 資料的圖片屬性
                    viewModel.Image = imageFileName;
                }
                else
                {
                    // 沒有上傳新圖片，保留原有的圖片
                    // 可以根據需要從資料庫或其他地方獲取原有圖片的路徑並設定給 viewModel.Image
                    Benz oringin = CARServices.GetBenzByID(id);
                    viewModel.Image = oringin.Image;
                }

                // 將 BenzViewModel 的屬性映射回 Benz 資料
                Benz benz = new Benz
                {
                    ID = viewModel.ID,
                    Image = viewModel.Image,
                    Model = viewModel.Model,
                    Money = viewModel.Money
                };

                // 儲存更新後的 Benz 資料到資料庫
                CARDBServices dbServices = new CARDBServices();
                dbServices.UpdateBenz(benz);

                return RedirectToAction("Benz");
            }

            return View(viewModel);
        }
        public ActionResult EditBMW(int id)
        {
            // 根據 id 從資料庫獲取 BMW 資料
            CARDBServices dbServices = new CARDBServices();
            BMW BMW = dbServices.GetBMWByID(id);

            // 將 BMW 資料映射到 BMWViewModel
            BMWViewModel viewModel = new BMWViewModel
            {
                ID = BMW.ID,
                Image = BMW.Image,
                Model = BMW.Model,
                Money = BMW.Money
            };

            return View(viewModel);
        }

        // POST: SaveInfoBMW
        [HttpPost]
        public ActionResult SaveInfoBMW(int id, BMWViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // 檢查圖片是否有變更
                if (viewModel.NewImage != null && viewModel.NewImage.ContentLength > 0)
                {
                    // 上傳新圖片並獲取保存的路徑
                    string serverPath = Server.MapPath("~/Images/");
                    string imageFileName = Path.GetFileName(viewModel.NewImage.FileName);
                    string saveUrl = serverPath + imageFileName;
                    viewModel.NewImage.SaveAs(saveUrl);

                    // 更新 BMW 資料的圖片屬性
                    viewModel.Image = imageFileName;
                }
                else
                {
                    // 沒有上傳新圖片，保留原有的圖片
                    // 可以根據需要從資料庫或其他地方獲取原有圖片的路徑並設定給 viewModel.Image
                    BMW oringin = CARServices.GetBMWByID(id);
                    viewModel.Image = oringin.Image;
                }

                // 將 BMWViewModel 的屬性映射回 BMW 資料
                BMW BMW = new BMW
                {
                    ID = viewModel.ID,
                    Image = viewModel.Image,
                    Model = viewModel.Model,
                    Money = viewModel.Money
                };

                // 儲存更新後的 BMW 資料到資料庫
                CARDBServices dbServices = new CARDBServices();
                dbServices.UpdateBMW(BMW);

                return RedirectToAction("BMW");
            }

            return View(viewModel);
        }

    }
}