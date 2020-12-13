using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AccountWebApp.Data;
using AccountWebApp.Models;

namespace AccountWebApp.Controllers
{

    public class AccountController : Controller
    {
        // create the connection with the use of constractor from the startup


        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Account
        public IActionResult Index()
        {

            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("AccountViewAll", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dtbl);
            }
            return View(dtbl);
        }



        // GET: Account/AddOrEdit/5
        public IActionResult AddOrEdit(int? id)
        {
            AccountViewModel accountViewModel = new AccountViewModel();

            if (id > 0)
                accountViewModel = FetchAccountById(id);

            return View(accountViewModel);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, [Bind("RefNo,AccountNumber,AccountHolder,CurrentBalance,BankName,OpeningDate,IsActive")] AccountViewModel accountViewModel)
        {
            // all update and add are here
            if (ModelState.IsValid)
            {

                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("AccountAddOrEdit", sqlConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("RefNo", accountViewModel.RefNo);
                    sqlCmd.Parameters.AddWithValue("AccountNumber", accountViewModel.AccountNumber);
                    sqlCmd.Parameters.AddWithValue("AccountHolder", accountViewModel.AccountHolder);
                    sqlCmd.Parameters.AddWithValue("CurrentBalance", accountViewModel.CurrentBalance);
                    sqlCmd.Parameters.AddWithValue("BankName", accountViewModel.BankName);
                    sqlCmd.Parameters.AddWithValue("OpeningDate", accountViewModel.OpeningDate.Date.ToLongDateString());
                    sqlCmd.Parameters.AddWithValue("IsActive", accountViewModel.IsActive);
                    sqlCmd.ExecuteNonQuery();

                }

                return RedirectToAction(nameof(Index));
            }

            return View(accountViewModel);
        }

        // GET: Account/Delete/5
        public IActionResult Delete(int? id)
        {

            AccountViewModel accountViewModel = FetchAccountById(id);

            return View(accountViewModel);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("AccountDeleteByRefNo", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("RefNo", id);

                sqlCmd.ExecuteNonQuery();

            }

            return RedirectToAction(nameof(Index));


        }


        public AccountViewModel FetchAccountById(int? id)
        {
            AccountViewModel accountViewModel = new AccountViewModel();

            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {

                DataTable dtbl = new DataTable();
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("AccountViewByRefNo", sqlConnection);

                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("RefNo", id);
                sqlDa.Fill(dtbl);

                if (dtbl.Rows.Count == 1)
                {
                    accountViewModel.RefNo = Convert.ToInt32(dtbl.Rows[0]["RefNo"].ToString());
                    accountViewModel.AccountNumber = dtbl.Rows[0]["AccountNumber"].ToString();
                    accountViewModel.AccountHolder = dtbl.Rows[0]["AccountHolder"].ToString();
                    accountViewModel.CurrentBalance = Convert.ToInt32(dtbl.Rows[0]["CurrentBalance"].ToString());
                    accountViewModel.BankName = dtbl.Rows[0]["BankName"].ToString();
                    accountViewModel.OpeningDate = Convert.ToDateTime(dtbl.Rows[0]["OpeningDate"].ToString());
                    accountViewModel.IsActive = Convert.ToBoolean(dtbl.Rows[0]["IsActive"].ToString());


                }
                return accountViewModel;

            }
        }

    }
}
