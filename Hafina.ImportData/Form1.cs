using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Hafina.ImportData
{
    public partial class Form1 : Form
    {
        HafinaContext dbContext = new HafinaContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            string fileName = "";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Load Excel File";
            fileDialog.FileName = "";
            fileDialog.Filter = "Excel File|*.xlsx;*.xls;*.csv";
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = fileDialog.FileName;
            }

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileName);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            var rowCount = xlRange.Rows.Count;
            var colCount = xlRange.Columns.Count;
            var financialReport = new FinancialReport();
            var balanceSheets = new List<BalanceSheet>();
            var businessResults = new List<BusinessResult>();

            for (int i = 2; i <= colCount; i++)
            {
                var balanceSheet = new BalanceSheet() { CompanyId = 3, CreatedAt = DateTime.Now };
                var businessResult = new BusinessResult() { CompanyId = 3, CreatedAt = DateTime.Now };

                for (int j = 1; j < rowCount; j++)
                {
                    if (xlRange.Cells[j, i] != null && xlRange.Cells[j, i].Value2 != null)
                    {
                        var value = xlRange.Cells[j, i].Value2;

                        balanceSheet.Duration = j == 1 ? value.ToString() : balanceSheet.Duration;
                        balanceSheet.CurrentAssets = j == 2 ? (decimal)value : balanceSheet.CurrentAssets;
                        balanceSheet.CashAndCashEuivalents = j == 3 ? (decimal)value : balanceSheet.CashAndCashEuivalents;
                        balanceSheet.ShortTermFinancialInvestment = j == 4 ? (decimal)value : balanceSheet.ShortTermFinancialInvestment;
                        balanceSheet.ShortTermAccountReceivables = j == 5 ? (decimal)value : balanceSheet.ShortTermAccountReceivables;
                        balanceSheet.Inventory = j == 6 ? (decimal)value : balanceSheet.Inventory;
                        balanceSheet.OtherCurrentAssets = j == 7 ? (decimal)value : balanceSheet.OtherCurrentAssets;
                        balanceSheet.LongTermAssets = j == 8 ? (decimal)value : balanceSheet.LongTermAssets;
                        balanceSheet.LongTermAccountReceivable = j == 9 ? (decimal)value : balanceSheet.LongTermAccountReceivable;
                        balanceSheet.FixedAssets = j == 10 ? (decimal)value : balanceSheet.FixedAssets;
                        balanceSheet.CommercialAdvantage = j == 14 ? (decimal)value : balanceSheet.CommercialAdvantage;
                        balanceSheet.RealEstateInvestment = j == 15 ? (decimal)value : balanceSheet.RealEstateInvestment;
                        balanceSheet.LongTermFinacialInvestments = j == 16 ? (decimal)value : balanceSheet.LongTermFinacialInvestments;
                        balanceSheet.OtherLongTermAssets = j == 17 ? (decimal)value : balanceSheet.OtherLongTermAssets;
                        balanceSheet.TotalAssets = j == 18 ? (decimal)value : balanceSheet.TotalAssets;
                        balanceSheet.ShortTermLiabilities = j == 20 ? (decimal)value : balanceSheet.ShortTermLiabilities;
                        balanceSheet.LongTermLiabilities = j == 21 ? (decimal)value : balanceSheet.LongTermLiabilities;
                        balanceSheet.OwnersEquity = j == 24 ? (decimal)value : balanceSheet.OwnersEquity;

                        businessResult.Duration = j == 32 ? value.ToString() : businessResult.Duration;
                        businessResult.SalesOfGoodsAndServices = j == 33 ? (decimal)value : businessResult.SalesOfGoodsAndServices;
                        businessResult.DeductibleRevenue = j == 34 ? (decimal)value : businessResult.DeductibleRevenue;
                        businessResult.NetSalesOfGoodsAndServices = j == 35 ? (decimal)value : businessResult.NetSalesOfGoodsAndServices;
                        businessResult.CostOfGoodsSold = j == 36 ? (decimal)value : businessResult.CostOfGoodsSold;
                        businessResult.GrossProfitOfGoodsAndServices = j == 37 ? (decimal)value : businessResult.GrossProfitOfGoodsAndServices;
                        businessResult.RevenueFromFinancialActivities = j == 38 ? (decimal)value : businessResult.RevenueFromFinancialActivities;
                        businessResult.FinancialExpenses = j == 39 ? (decimal)value : businessResult.FinancialExpenses;
                        businessResult.SellingExpenses = j == 40 ? (decimal)value : businessResult.SellingExpenses;
                        businessResult.EnterpriseCostManagement = j == 41 ? (decimal)value : businessResult.EnterpriseCostManagement;
                        businessResult.NetProfitFromBusinessActivities = j == 42 ? (decimal)value : businessResult.NetProfitFromBusinessActivities;
                        businessResult.OtherIncome = j == 43 ? (decimal)value : businessResult.OtherIncome;
                        businessResult.OtherCosts = j == 44 ? (decimal)value : businessResult.OtherCosts;
                        businessResult.OtherProfits = j == 45 ? (decimal)value : businessResult.OtherProfits;
                        businessResult.AccountingProfitBeforeTax = j == 46 ? (decimal)value : businessResult.AccountingProfitBeforeTax;
                        businessResult.CurrentCorporateIncomeTaxExpense = j == 48 ? (decimal)value : businessResult.CurrentCorporateIncomeTaxExpense;
                        businessResult.DeferredCorporateIncomeTaxExpense = j == 49 ? (decimal)value : businessResult.DeferredCorporateIncomeTaxExpense;
                        businessResult.ProfitAfterTaxCorporateIncome = j == 50 ? (decimal)value : businessResult.ProfitAfterTaxCorporateIncome;
                    }
                }
                dbContext.Set<BalanceSheet>().Add(balanceSheet);
                dbContext.Set<BusinessResult>().Add(businessResult);
                balanceSheets.Add(balanceSheet);
                businessResults.Add(businessResult);
            }

            CleanSetting(xlApp, xlWorkbook, xlWorksheet, xlRange);

            dbContext.SaveChanges();
            MessageBox.Show("Import Data is successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CleanSetting(Excel.Application xlApp, Excel.Workbook xlWorkbook, Excel._Worksheet xlWorksheet, Excel.Range xlRange)
        {
            //cleanup  
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:  
            //  never use two dots, all COM objects must be referenced and released individually  
            //  ex: [somthing].[something].[something] is bad  

            //release com objects to fully kill excel process from running in the background  
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release  
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release  
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        private void btnSetCompany_Click(object sender, EventArgs e)
        {
            var companies = new List<Company>() {
                new Company()
                {
                    Code = "REE",
                    Name = "Công ty Cổ phần Cơ điện lạnh (HOSE)",
                    CreatedAt = DateTime.Now
                },
                new Company()
                {
                    Code = "VIC",
                    Name = "Tập đoàn Vingroup - Công ty Cổ phần (HOSE)",
                    CreatedAt = DateTime.Now
                },
                new Company()
                {
                    Code = "MSN",
                    Name = "Công ty Cổ phần Tập đoàn MaSan (HOSE)",
                    CreatedAt = DateTime.Now
                }
            };
            dbContext.Set<Company>().AddRange(companies);
            dbContext.SaveChanges();
        }
    }
}
