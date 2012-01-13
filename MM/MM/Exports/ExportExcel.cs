﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using SpreadsheetGear;
using SpreadsheetGear.Shapes;

namespace MM.Exports
{
    public class ExportExcel
    {
        public static bool ExportReceiptToExcel(string exportFileName, string receiptGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                Result result = ReceiptBus.GetReceipt(receiptGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetReceipt"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceipt"));
                    return false;
                }

                ReceiptView receipt = result.QueryResult as ReceiptView;
                if (receipt == null) return false;

                result = ReceiptBus.GetReceiptDetailList(receiptGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\ReceiptTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["A2"].Value = string.Format("Số: {0}", receipt.ReceiptCode);
                workSheet.Cells["B6"].Value = string.Format("Họ tên: {0}", receipt.FullName);
                workSheet.Cells["B7"].Value = string.Format("Mã bệnh nhân: {0}", receipt.FileNum);
                workSheet.Cells["B8"].Value = string.Format("Ngày: {0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                if (receipt.Address != null) workSheet.Cells["B9"].Value = string.Format("Địa chỉ: {0}", receipt.Address);
                else workSheet.Cells["B9"].Value = "Địa chỉ:";

                int rowIndex = 11;
                int no = 1;
                double totalPrice = 0;
                IRange range;
                DataTable dtSource = result.QueryResult as DataTable;
                foreach (DataRow row in dtSource.Rows)
                {
                    string serviceName = row["Name"].ToString();
                    double price = Convert.ToDouble(row["Price"]);
                    double disCount = Convert.ToDouble(row["Discount"]);
                    double amount = Convert.ToDouble(row["Amount"]);
                    totalPrice += amount;
                    workSheet.Cells[rowIndex, 1].Value = no++;
                    workSheet.Cells[rowIndex, 1].HorizontalAlignment = HAlign.Center;

                    workSheet.Cells[rowIndex, 2].Value = serviceName;

                    workSheet.Cells[rowIndex, 3].Value = 1;
                    workSheet.Cells[rowIndex, 3].HorizontalAlignment = HAlign.Right;

                    if (price > 0)
                        workSheet.Cells[rowIndex, 4].Value = price.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 4].Value = price.ToString();

                    workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Right;

                    if (disCount > 0)
                        workSheet.Cells[rowIndex, 5].Value = disCount.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 5].Value = disCount.ToString();

                    workSheet.Cells[rowIndex, 5].HorizontalAlignment = HAlign.Right;

                    if (amount > 0)
                        workSheet.Cells[rowIndex, 6].Value = amount.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 6].Value = amount.ToString();

                    workSheet.Cells[rowIndex, 6].HorizontalAlignment = HAlign.Right;

                    range = workSheet.Cells[string.Format("B{0}:G{0}", rowIndex + 1)];
                    range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dash;
                    range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeLeft].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeLeft].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                    range.Borders[BordersIndex.InsideVertical].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.InsideVertical].Color = Color.Black;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                range.Value = "Tổng cộng:";
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                if (totalPrice > 0)
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString("#,###"));
                else
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString());

                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Right;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("B{0}", rowIndex + 1)];
                range.Value = string.Format("Bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPrice));
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("B{0}:G{0}", rowIndex + 1)];
                range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dash;
                range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                range.Value = "Người lập phiếu";
                range.HorizontalAlignment = HAlign.Left;

                range = workSheet.Cells[string.Format("D{0}", rowIndex + 1)];
                range.Value = "Người nộp tiền";
                range.HorizontalAlignment = HAlign.Left;

                range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                range.Value = "Thu ngân";
                range.HorizontalAlignment = HAlign.Left;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportSymptomToExcel(string exportFileName, List<DataRow> checkedRows)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\SymptomTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 1;

                foreach (DataRow row in checkedRows)
                {
                    string symptom = row["SymptomName"].ToString();
                    string advice = row["Advice"].ToString();
                    workSheet.Cells[rowIndex, 0].Value = rowIndex;
                    workSheet.Cells[rowIndex, 1].Value = symptom.Replace("\r", "").Replace("\t", "");
                    workSheet.Cells[rowIndex, 2].Value = advice.Replace("\r", "").Replace("\t", "");
                    rowIndex++;
                }

                IRange range = workSheet.Cells[string.Format("A2:C{0}", checkedRows.Count + 1)];
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                range = workSheet.Cells[string.Format("A2:A{0}", checkedRows.Count + 1)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportInvoiceToExcel(string exportFileName, string invoiceGUID, string lien)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                Result result = InvoiceBus.GetInvoice(invoiceGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.GetInvoice"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.GetInvoice"));
                    return false;
                }

                InvoiceView invoice = result.QueryResult as InvoiceView;
                if (invoice == null) return false;

                result = InvoiceBus.GetInvoiceDetailList(invoice.ReceiptGUID.ToString());
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.GetInvoiceDetailList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.GetInvoiceDetailList"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\HDGTGTTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["E3"].Value = string.Format("Số: {0}", invoice.InvoiceCode);

                DateTime dt = DateTime.Now;
                string strDay = dt.Day >= 10 ? dt.Day.ToString() : string.Format("0{0}", dt.Day);
                string strMonth = dt.Month >= 10 ? dt.Month.ToString() : string.Format("0{0}", dt.Month);
                string strYear = dt.Year.ToString();

                workSheet.Cells["A3"].Value = lien;
                workSheet.Cells["A4"].Value = string.Format("                                   Ngày {0} tháng {1} năm {2}", strDay, strMonth, strYear);
                workSheet.Cells["A11"].Value = string.Format("  Họ tên người mua hàng: {0}", invoice.FullName);
                workSheet.Cells["A12"].Value = string.Format("  Tên đơn vị: {0}", invoice.TenDonVi);
                workSheet.Cells["A13"].Value = string.Format("  Địa chỉ: {0}", invoice.Address);
                workSheet.Cells["A14"].Value = string.Format("  Số tài khoản: {0}", invoice.SoTaiKhoan);
                workSheet.Cells["A15"].Value = string.Format("  Hình thức thanh toán: {0}", invoice.HinhThucThanhToanStr);

                IRange range = null;
                DataTable dataSource = result.QueryResult as DataTable;
                foreach (DataRow row in dataSource.Rows)
                {
                    range = workSheet.Cells["A18"].EntireRow;
                    range.Insert(InsertShiftDirection.Down);
                }

                int no = 1;
                int rowIndex = 17;
                double totalPrice = 0;
                foreach (DataRow row in dataSource.Rows)
                {
                    string serviceName = row["Name"].ToString();
                    string donViTinh = row["DonViTinh"].ToString();
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    int donGia = Convert.ToInt32(row["DonGia"]);
                    double thanhTien = Convert.ToDouble(row["ThanhTien"]);
                    totalPrice += thanhTien;

                    range = workSheet.Cells[rowIndex, 0];
                    range.Value = no++;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 1];
                    range.Value = serviceName;
                    range.HorizontalAlignment = HAlign.Left;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 2];
                    range.Value = donViTinh;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 3];
                    range.Value = soLuong;
                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 4];
                    if (donGia > 0)
                        range.Value = donGia.ToString("#,###");
                    else
                        range.Value = donGia.ToString();

                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 5];
                    if (thanhTien > 0)
                        range.Value = thanhTien.ToString("#,###");
                    else
                        range.Value = thanhTien.ToString();

                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                if (totalPrice > 0)
                    range.Value = totalPrice.ToString("#,###");
                else
                    range.Value = totalPrice.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                range.Value = string.Format("  Thuế suất GTGT: {0}%, Tiền thuế GTGT:", invoice.VAT);

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                double vat = (invoice.VAT.Value * totalPrice) / 100;
                vat = Math.Round(vat + 0.05);
                if (vat > 0)
                    range.Value = vat.ToString("#,###");
                else
                    range.Value = vat.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                double totalPayment = totalPrice + vat;
                if (totalPayment > 0)
                    range.Value = totalPayment.ToString("#,###");
                else
                    range.Value = totalPayment.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                range.Value = string.Format("  Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPayment).ToUpper());

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportToaThuocToExcel(string exportFileName, string toaThuocGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Empty;
            IWorkbook workBook = null;

            try
            {
                Result result = KeToaBus.GetToaThuoc(toaThuocGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaBus.GetToaThuoc"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetToaThuoc"));
                    return false;
                }

                ToaThuocView toaThuoc = result.QueryResult as ToaThuocView;
                if (toaThuoc == null) return false;

                result = KeToaBus.GetChiTietToaThuocList(toaThuocGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaBus.GetChiTietToaThuocList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetChiTietToaThuocList"));
                    return false;
                }

                

                if (toaThuoc.Loai == (byte)LoaiToaThuoc.SanKhoa)
                {
                    excelTemplateName = string.Format("{0}\\Templates\\ToaThuocSanKhoaTemplate.xls", Application.StartupPath);
                    workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                    IWorksheet workSheet = workBook.Worksheets[0];

                    IRange range = workSheet.Cells["A2"];
                    range.Value = string.Format("Full name: {0}", toaThuoc.TenBenhNhan);
                    
                    range = workSheet.Cells["C2"];
                    range.Value = string.Format("SEX: {0}", toaThuoc.GenderAsStr);

                    range = workSheet.Cells["D2"];
                    range.Value = string.Format("              D.O.B: {0}", toaThuoc.DobStr);

                    range = workSheet.Cells["A4"];
                    range.Value = string.Format("Address: {0}", toaThuoc.Address);

                    range = workSheet.Cells["D4"];
                    range.Value = string.Format("              Tel: {0}", toaThuoc.Mobile);

                    range = workSheet.Cells["A6"];
                    range.Value = string.Format("Clinical diagnosis: {0}", toaThuoc.ChanDoan.Replace("\r", "").Replace("\t", ""));

                    range = workSheet.Cells["D6"];
                    range.Value = string.Format("              Date of Checking: {0}", toaThuoc.NgayKham.Value.ToString("dd/MM/yyyy"));

                    int rowIndex = 9;

                    DataTable dt = result.QueryResult as DataTable;
                    int stt = 1;
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        string tenThuoc = row["TenThuoc"].ToString();
                        int soLuong = Convert.ToInt32(row["SoLuong"]);
                        string lieuDung = row["LieuDung"].ToString();
                        string note = row["Note"] as string;

                        range = workSheet.Cells[rowIndex, 0];
                        range.Value = stt;
                        range.HorizontalAlignment = HAlign.Center;
                        range.VerticalAlignment = VAlign.Top;

                        range = workSheet.Cells[rowIndex, 1];
                        range.Value = tenThuoc;
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;

                        range = workSheet.Cells[rowIndex, 2];
                        range.Value = soLuong;
                        range.HorizontalAlignment = HAlign.Center;
                        range.VerticalAlignment = VAlign.Top;

                        range = workSheet.Cells[rowIndex, 3];
                        range.Value = lieuDung.Replace("\r", "").Replace("\t", "");
                        range.WrapText = true;
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;

                        range = workSheet.Cells[rowIndex, 4];
                        range.Value = note.Replace("\r", "").Replace("\t", "");
                        range.WrapText = true;
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;

                        rowIndex++;
                        stt++;
                    }

                    range = workSheet.Cells[string.Format("A10:E{0}", dt.Rows.Count + 9)];
                    range.Borders.Color = Color.Black;
                    range.Borders.LineStyle = LineStyle.Continuous;
                    range.Borders.Weight = BorderWeight.Thin;

                    range = workSheet.Cells[string.Format("D{0}", dt.Rows.Count + 11)];
                    range.Value = "                    Doctor's signature";
                    range.Font.Bold = true;
                    range.HorizontalAlignment = HAlign.Center;
                    range.VerticalAlignment = VAlign.Top;

                    range = workSheet.Cells[string.Format("D{0}", dt.Rows.Count + 12)];
                    range.Value = "                    (BS điều trị)";
                    range.HorizontalAlignment = HAlign.Center;
                    range.VerticalAlignment = VAlign.Top;
                }
                else
                {
                    excelTemplateName = string.Format("{0}\\Templates\\ToaThuocChungTemplate.xls", Application.StartupPath);
                    workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                    IWorksheet workSheet = workBook.Worksheets[0];

                    IRange range = workSheet.Cells["D2"];
                    range.Value = toaThuoc.TenBenhNhan;

                    range = workSheet.Cells["C3"];
                    range.Value = toaThuoc.DobStr;

                    if (toaThuoc.GenderAsStr == "Nam")
                        workSheet.Shapes[1].ControlFormat.Value = 1;
                    else
                        workSheet.Shapes[0].ControlFormat.Value = 1;

                    range = workSheet.Cells["C4"];
                    range.Value = toaThuoc.Address;

                    range = workSheet.Cells["C6"];
                    range.Value = toaThuoc.ChanDoan.Replace("\r", "").Replace("\t", "");

                    int rowIndex = 7;
                    DataTable dt = result.QueryResult as DataTable;
                    int stt = 1;
                    IShape shape = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        string tenThuoc = row["TenThuoc"].ToString();
                        int soLuong = Convert.ToInt32(row["SoLuong"]);
                        bool Sang = Convert.ToBoolean(row["Sang"]);
                        bool Trua = Convert.ToBoolean(row["Trua"]);
                        bool Chieu = Convert.ToBoolean(row["Chieu"]);
                        bool Toi = Convert.ToBoolean(row["Toi"]);
                        bool TruocAn = Convert.ToBoolean(row["TruocAn"]);
                        bool SauAn = Convert.ToBoolean(row["SauAn"]);
                        bool Khac_TruocSauAn = Convert.ToBoolean(row["Khac_TruocSauAn"]);
                        bool Uong = Convert.ToBoolean(row["Uong"]);
                        bool Boi = Convert.ToBoolean(row["Boi"]);
                        bool DatAD = Convert.ToBoolean(row["Dat"]);
                        bool Khac_CachDung = Convert.ToBoolean(row["Khac_CachDung"]);
                        string SangNote = row["SangNote"].ToString();
                        string TruaNote = row["TruaNote"].ToString();
                        string ChieuNote = row["ChieuNote"].ToString();
                        string ToiNote = row["ToiNote"].ToString();
                        string TruocAnNote = row["TruocAnNote"].ToString();
                        string SauAnNote = row["SauAnNote"].ToString();
                        string Khac_TruocSauAnNote = row["Khac_TruocSauAnNote"].ToString();
                        string UongNote = row["UongNote"].ToString();
                        string BoiNote = row["BoiNote"].ToString();
                        string DatADNote = row["DatNote"].ToString();
                        string Khac_CachDungNote = row["Khac_CachDungNote"].ToString();

                        range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                        range.Value = string.Format("{0}. {1}", stt, tenThuoc);

                        range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                        range.Value = "Số lượng (Quantity):";

                        range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                        range.Value = soLuong;

                        rowIndex++;

                        range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Sáng (AM)";

                        range = workSheet.Cells[string.Format("B{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = SangNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox, 
                            workSheet.WindowInfo.ColumnToPoints(1) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty; 
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Sang ? 1 : 0;

                        range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Trưa (Noon)";

                        range = workSheet.Cells[string.Format("D{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = TruaNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(3) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Trua ? 1 : 0;

                        range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Chiều (PM)";

                        range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = ChieuNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(5) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Chieu ? 1 : 0;

                        range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Tối (Night)";

                        range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = ToiNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(7) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Toi ? 1 : 0;

                        rowIndex++;

                        range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Trước ăn (Before meal)";

                        range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = TruocAnNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(2) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = TruocAn ? 1 : 0;

                        range = workSheet.Cells[string.Format("D{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Sau ăn (After meal)";

                        range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = SauAnNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(5) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = SauAn ? 1 : 0;

                        range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Khác (Others)";

                        range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = Khac_TruocSauAnNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(7) + 13, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Khac_TruocSauAn ? 1 : 0;

                        rowIndex++;

                        range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Uống";

                        range = workSheet.Cells[string.Format("B{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = UongNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(1) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Uong ? 1 : 0;

                        range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Bôi";

                        range = workSheet.Cells[string.Format("D{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = BoiNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(3) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Boi ? 1 : 0;

                        range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Đặt AĐ";

                        range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = DatADNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(5) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = DatAD ? 1 : 0;

                        range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Khác";

                        range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = Khac_CachDungNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(7) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Khac_CachDung ? 1 : 0;

                        rowIndex++;

                        range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "(Take)";

                        range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "(Apply)";

                        range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "(Put inside vagina)";

                        range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "(Orthers)";

                        stt++;
                        rowIndex += 2;
                    }

                    range = workSheet.Cells[string.Format("A{0}:H{1}", rowIndex + 1, rowIndex + 3)];
                    range.Merge();
                    range.WrapText = true;
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.Value = string.Format("Lời dặn (Recommendation): {0}", toaThuoc.Note.Replace("\r", "").Replace("\t", ""));

                    rowIndex += 3;

                    range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                    range.Value = string.Format("* Tái khám ngày (Re-examination date): {0}", toaThuoc.NgayTaiKham.Value.ToString("dd/MM/yyyy"));
                    rowIndex++;

                    range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                    range.Value = "* Khi tái khám vui lòng mang theo toa này (Please, keep it belong you for re-examination).";
                    rowIndex++;

                    range = workSheet.Cells[string.Format("F{0}:G{0}", rowIndex + 1)];
                    range.Merge();
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.Value = string.Format("Date: {0}", toaThuoc.NgayKham.Value.ToString("dd/MM/yyyy"));
                    rowIndex++;

                    range = workSheet.Cells[string.Format("F{0}:G{0}", rowIndex + 1)];
                    range.Merge();
                    range.HorizontalAlignment = HAlign.Center;
                    range.VerticalAlignment = VAlign.Top;
                    range.Font.Bold = true;
                    range.Value = "Doctor's signature";
                    rowIndex++;

                    range = workSheet.Cells[string.Format("F{0}:G{0}", rowIndex + 1)];
                    range.Merge();
                    range.HorizontalAlignment = HAlign.Center;
                    range.VerticalAlignment = VAlign.Top;
                    range.Value = "(BS điều trị)";
                }

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportPhieuThuThuocToExcel(string exportFileName, string phieuThuThuocGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                Result result = PhieuThuThuocBus.GetPhieuThuThuoc(phieuThuThuocGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuoc"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuoc"));
                    return false;
                }

                PhieuThuThuoc ptThuoc = result.QueryResult as PhieuThuThuoc;
                if (ptThuoc == null) return false;

                result = PhieuThuThuocBus.GetChiTietPhieuThuThuoc(phieuThuThuocGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.GetChiTietPhieuThuThuoc"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetChiTietPhieuThuThuoc"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\PhieuThuThuocTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["A2"].Value = string.Format("Số: {0}", ptThuoc.MaPhieuThuThuoc);
                workSheet.Cells["B6"].Value = string.Format("Họ tên: {0}", ptThuoc.TenBenhNhan);
                workSheet.Cells["B7"].Value = string.Format("Mã bệnh nhân: {0}", ptThuoc.MaBenhNhan);
                workSheet.Cells["B8"].Value = string.Format("Ngày: {0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                if (ptThuoc.DiaChi != null) workSheet.Cells["B9"].Value = string.Format("Địa chỉ: {0}", ptThuoc.DiaChi);
                else workSheet.Cells["B9"].Value = "Địa chỉ:";

                int rowIndex = 11;
                int no = 1;
                double totalPrice = 0;
                IRange range;
                DataTable dtSource = result.QueryResult as DataTable;
                foreach (DataRow row in dtSource.Rows)
                {
                    string tenThuoc = row["TenThuoc"].ToString();
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    string donViTinh = row["DonViTinh"].ToString();
                    double donGia = Convert.ToDouble(row["DonGia"]);
                    double giam = Convert.ToDouble(row["Giam"]);
                    double thanhTien = Convert.ToDouble(row["ThanhTien"]);

                    totalPrice += thanhTien;
                    workSheet.Cells[rowIndex, 1].Value = no++;
                    workSheet.Cells[rowIndex, 1].HorizontalAlignment = HAlign.Center;

                    workSheet.Cells[rowIndex, 2].Value = tenThuoc;

                    workSheet.Cells[rowIndex, 3].Value = soLuong;
                    workSheet.Cells[rowIndex, 3].HorizontalAlignment = HAlign.Right;

                    workSheet.Cells[rowIndex, 4].Value = donViTinh;
                    workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Center;

                    if (donGia > 0)
                        workSheet.Cells[rowIndex, 5].Value = donGia.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 5].Value = donGia.ToString();

                    workSheet.Cells[rowIndex, 5].HorizontalAlignment = HAlign.Right;

                    if (giam > 0)
                        workSheet.Cells[rowIndex, 6].Value = giam.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 6].Value = giam.ToString();

                    workSheet.Cells[rowIndex, 6].HorizontalAlignment = HAlign.Right;

                    if (thanhTien > 0)
                        workSheet.Cells[rowIndex, 7].Value = thanhTien.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 7].Value = thanhTien.ToString();

                    workSheet.Cells[rowIndex, 7].HorizontalAlignment = HAlign.Right;

                    range = workSheet.Cells[string.Format("B{0}:H{0}", rowIndex + 1)];
                    range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dash;
                    range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeLeft].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeLeft].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                    range.Borders[BordersIndex.InsideVertical].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.InsideVertical].Color = Color.Black;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.Value = "Tổng cộng:";
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                if (totalPrice > 0)
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString("#,###"));
                else
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString());

                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Right;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("B{0}", rowIndex + 1)];
                range.Value = string.Format("Bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPrice));
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("B{0}:H{0}", rowIndex + 1)];
                range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dash;
                range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                range.Value = "Người lập phiếu";
                range.HorizontalAlignment = HAlign.Left;

                range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                range.Value = "Người nộp tiền";
                range.HorizontalAlignment = HAlign.Left;

                range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                range.Value = "Thu ngân";
                range.HorizontalAlignment = HAlign.Left;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportDichVuHopDongToExcel(string exportFileName, List<spDichVuHopDongResult> results)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\DichVuHopDongTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                
                string tenHopDong = results[0].ContractName;
                DateTime tuNgay = results[0].TuNgay.Value;
                DateTime denNgay = results[0].DenNgay.Value;

                workSheet.Cells["B2"].Value = string.Format("Hợp đồng: {0}", tenHopDong);
                workSheet.Cells["B3"].Value = string.Format("Từ ngày: {0} Đến ngày: {1}", tuNgay.ToString("dd/MM/yyyy"), denNgay.ToString("dd/MM/yyyy"));

                int rowIndex = 5;
                int stt = 1;
                foreach (var result in results)
                {
                    workSheet.Cells[rowIndex, 0].Value = stt;
                    workSheet.Cells[rowIndex, 1].Value = result.FullName;
                    if (result.NgayKham.HasValue)
                        workSheet.Cells[rowIndex, 2].Value = result.NgayKham.Value.ToString("dd/MM/yyyy");

                    workSheet.Cells[rowIndex, 3].Value = result.Mobile;
                    workSheet.Cells[rowIndex, 4].Value = result.TinhTrang;
                    rowIndex++;
                    stt++;
                }

                IRange range = workSheet.Cells[string.Format("A5:E{0}", results.Count + 5)];
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                range = workSheet.Cells[string.Format("A5:A{0}", results.Count + 5)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                range = workSheet.Cells[string.Format("C5:C{0}", results.Count + 5)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                range = workSheet.Cells[string.Format("E5:E{0}", results.Count + 5)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportDichVuTuTucToExcel(string exportFileName, List<spDichVuTuTucResult> results)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\DichVuTuTucTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];

                DateTime tuNgay = results[0].TuNgay.Value;
                DateTime denNgay = results[0].DenNgay.Value;

                workSheet.Cells["B2"].Value = string.Format("Từ ngày: {0} Đến ngày: {1}", tuNgay.ToString("dd/MM/yyyy"), denNgay.ToString("dd/MM/yyyy"));

                int rowIndex = 4;
                int stt = 1;
                foreach (var result in results)
                {
                    workSheet.Cells[rowIndex, 0].Value = stt;
                    workSheet.Cells[rowIndex, 1].Value = result.FullName;
                    if (result.NgayKham.HasValue)
                        workSheet.Cells[rowIndex, 2].Value = result.NgayKham.Value.ToString("dd/MM/yyyy");

                    workSheet.Cells[rowIndex, 3].Value = result.Mobile;
                    workSheet.Cells[rowIndex, 4].Value = result.CompanyName;
                    rowIndex++;
                    stt++;
                }

                IRange range = workSheet.Cells[string.Format("A5:E{0}", results.Count + 4)];
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                range = workSheet.Cells[string.Format("A5:A{0}", results.Count + 4)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                range = workSheet.Cells[string.Format("C5:C{0}", results.Count + 4)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportDanhSachBenhNhanToExcel(string exportFileName, List<DataRow> checkedRows)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\DanhSachBenhNhanTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                int stt = 1;

                foreach (DataRow row in checkedRows)
                {
                    string maBenhNhan = row["FileNum"].ToString();
                    string tenBenhNhan = row["FullName"].ToString();
                    string ngaySinh = row["DobStr"].ToString();
                    string gioiTinh = row["GenderAsStr"].ToString();

                    workSheet.Cells[rowIndex, 0].Value = stt;
                    workSheet.Cells[rowIndex, 1].Value = maBenhNhan;
                    workSheet.Cells[rowIndex, 2].Value = tenBenhNhan;
                    workSheet.Cells[rowIndex, 3].Value = ngaySinh;
                    workSheet.Cells[rowIndex, 4].Value = gioiTinh;
                    rowIndex++;
                    stt++;
                }

                IRange range = workSheet.Cells[string.Format("A3:G{0}", checkedRows.Count + 2)];
                range.WrapText = false;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                range = workSheet.Cells[string.Format("A3:A{0}", checkedRows.Count + 2)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                range = workSheet.Cells[string.Format("C3:C{0}", checkedRows.Count + 2)];
                range.WrapText = true;

                range = workSheet.Cells[string.Format("D3:E{0}", checkedRows.Count + 2)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;
                range.ShrinkToFit = true;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportChiTietPhieuThuToExcel(string exportFileName, List<string> phieuThuKeyList)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\ChiTietPhieuThuTemplate.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                IRange range;
                foreach (string key in phieuThuKeyList)
                {
                    Result result = ReceiptBus.GetReceipt(key);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetReceipt"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceipt"));
                        return false;
                    }

                    ReceiptView receipt = result.QueryResult as ReceiptView;
                    if (receipt == null) continue;

                    result = CompanyBus.GetTenCongTy(key);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyBus.GetTenCongTy"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetTenCongTy"));
                        return false;
                    }

                    string tenCongTy = receipt.CompanyName;
                    if (tenCongTy == null) tenCongTy = "Tự túc";
                    if (result.QueryResult != null && result.QueryResult.ToString() != string.Empty)
                        tenCongTy = result.QueryResult.ToString();

                    result = ReceiptBus.GetReceiptDetailList(key);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"));
                        return false;
                    }

                    DataTable dtSource = result.QueryResult as DataTable;
                   
                    foreach (DataRow row in dtSource.Rows)
                    {
                        range = workSheet.Cells[rowIndex, 0];
                        range.Value = receipt.ReceiptCode;

                        range = workSheet.Cells[rowIndex, 1];
                        range.Value = receipt.ReceiptDate.ToString("dd/MM/yyyy");

                        range = workSheet.Cells[rowIndex, 2];
                        range.Value = tenCongTy;
                        range.WrapText = true;

                        range = workSheet.Cells[rowIndex, 3];
                        range.Value = receipt.FullName;
                        range.WrapText = true;

                        range = workSheet.Cells[rowIndex, 4];
                        range.Value = row["Name"].ToString();
                        range.WrapText = true;

                        range = workSheet.Cells[rowIndex, 5];
                        range.Value = Convert.ToDouble(row["Amount"]);

                        rowIndex++;
                    }
                }

                range = workSheet.Cells[string.Format("A3:F{0}", rowIndex)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportChiTietPhieuThuThuocToExcel(string exportFileName, List<string> phieuThuKeyList)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\ChiTietPhieuThuThuocTemplate.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                IRange range;
                foreach (string key in phieuThuKeyList)
                {
                    Result result = PhieuThuThuocBus.GetPhieuThuThuoc(key);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuoc"));
                        return false;
                    }

                    PhieuThuThuoc phieuThuThuoc = result.QueryResult as PhieuThuThuoc;
                    if (phieuThuThuoc == null) continue;
                    if (phieuThuThuoc.TenCongTy == null) phieuThuThuoc.TenCongTy = "Tự túc";

                    result = PhieuThuThuocBus.GetChiTietPhieuThuThuoc(key);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.GetChiTietPhieuThuThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetChiTietPhieuThuThuoc"));
                        return false;
                    }

                    DataTable dtSource = result.QueryResult as DataTable;

                    foreach (DataRow row in dtSource.Rows)
                    {
                        range = workSheet.Cells[rowIndex, 0];
                        range.Value = phieuThuThuoc.MaPhieuThuThuoc;

                        range = workSheet.Cells[rowIndex, 1];
                        range.Value = phieuThuThuoc.NgayThu.ToString("dd/MM/yyyy");

                        range = workSheet.Cells[rowIndex, 2];
                        range.Value = phieuThuThuoc.TenCongTy;
                        range.WrapText = true;

                        range = workSheet.Cells[rowIndex, 3];
                        range.Value = phieuThuThuoc.TenBenhNhan;
                        range.WrapText = true;

                        range = workSheet.Cells[rowIndex, 4];
                        range.Value = row["TenThuoc"].ToString();
                        range.WrapText = true;

                        range = workSheet.Cells[rowIndex, 5];
                        range.Value = Convert.ToDouble(row["ThanhTien"]);

                        rowIndex++;
                    }
                }

                range = workSheet.Cells[string.Format("A3:F{0}", rowIndex)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportKhamSucKhoeTongQuatToExcel(string exportFileName, DataRow patientRow, DateTime fromDate, DateTime toDate)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                //Lấy thông tin bệnh nhân
                string patientGUID = patientRow["PatientGUID"].ToString();
                string tenBenhNhan = patientRow["FullName"].ToString();
                string gioiTinh = patientRow["GenderAsStr"].ToString();
                string ngaySinh = patientRow["DobStr"].ToString();
                string diaChi = patientRow["Address"].ToString();

                Result result = CompanyBus.GetTenCongTy(patientGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyBus.GetTenCongTy"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetTenCongTy"));
                    return false;
                }

                string tenCongTy = "Tự túc";
                if (patientRow["CompanyName"] != null && patientRow["CompanyName"] != DBNull.Value)
                    tenCongTy = patientRow["CompanyName"].ToString();

                if (result.QueryResult != null && result.QueryResult.ToString() != string.Empty)
                    tenCongTy = result.QueryResult.ToString();

                //Lấy thông tin cân đo
                result = CanDoBus.GetLastCanDo(patientGUID, fromDate, toDate);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("CanDoBus.GetLastCanDo"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CanDoBus.GetLastCanDo"));
                    return false;
                }

                CanDo canDo = result.QueryResult as CanDo;

                //Lấy thông tin khám lâm sàng
                result = KetQuaLamSangBus.GetLastKetQuaLamSang(patientGUID, fromDate, toDate);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaLamSangBus.GetLastKetQuaLamSang"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaLamSangBus.GetLastKetQuaLamSang"));
                    return false;
                }

                Hashtable htKetQuaLamSang = result.QueryResult as Hashtable;

                //Lấy thông tin dịch vụ sử dụng lâm sàng
                result = ServiceHistoryBus.GetServiceHistory(patientGUID, fromDate, toDate, ServiceType.LamSang);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceHistoryBus.GetServiceHistory"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.GetServiceHistory"));
                    return false;
                }

                List<ServiceHistoryView> serviceLamSangList = (List<ServiceHistoryView>)result.QueryResult;

                //Lấy thông tin dịch vụ sử dụng cận lâm sàng
                result = ServiceHistoryBus.GetServiceHistory(patientGUID, fromDate, toDate, ServiceType.CanLamSang);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceHistoryBus.GetServiceHistory"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.GetServiceHistory"));
                    return false;
                }

                List<ServiceHistoryView> serviceCanLamSangList = (List<ServiceHistoryView>)result.QueryResult;

                //Lấy thông tin lời khuyên
                result = LoiKhuyenBus.GetLoiKhuyenList2(patientGUID, fromDate, toDate);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("LoiKhuyenBus.GetLoiKhuyenList2"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("LoiKhuyenBus.GetLoiKhuyenList2"));
                    return false;
                }

                List<LoiKhuyenView> loiKhuyenList = (List<LoiKhuyenView>)result.QueryResult;

                string excelTemplateName = string.Format("{0}\\Templates\\KhamSucKhoeTongQuatTemplate.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                IRange range;
                IShape shape;

                //Fill thông tin bệnh nhân
                workSheet.Cells["B2"].Value = tenBenhNhan;
                workSheet.Cells["F2"].Value = gioiTinh;
                workSheet.Cells["I2"].Value = ngaySinh;
                workSheet.Cells["B4"].Value = diaChi;
                workSheet.Cells["F4"].Value = tenCongTy;

                //Fill thông tin cân đo
                if (canDo != null)
                {
                    workSheet.Cells["A8"].Value = canDo.ChieuCao;
                    workSheet.Cells["B8"].Value = canDo.CanNang;
                    workSheet.Cells["C8"].Value = canDo.BMI;
                    workSheet.Cells["D8"].Value = canDo.HuyetAp;
                    workSheet.Cells["E8"].Value = canDo.TimMach;
                    workSheet.Cells["F8"].Value = canDo.HoHap;
                    workSheet.Cells["G8"].Value = canDo.MuMau;
                    workSheet.Cells["H7"].Value = string.Format("R(P): {0}\nL(T): {1}", canDo.MatTrai, canDo.MatTrai);

                    if (!canDo.HieuChinh) workSheet.Shapes[0].ControlFormat.Value = 1;
                    else workSheet.Shapes[1].ControlFormat.Value = 1;
                }

                //Fill thông tin khám lâm sàng
                CoQuan[] coQuanList = (CoQuan[])Enum.GetValues(typeof(CoQuan));
                int checkIndex = 4;
                int rowIndex = 10;
                foreach (CoQuan coQuan in coQuanList)
                {
                    if (htKetQuaLamSang.ContainsKey(coQuan))
                    {
                        KetQuaLamSang kq = (KetQuaLamSang)htKetQuaLamSang[coQuan];
                        if (kq.Note == null) kq.Note = string.Empty;
                        if (kq.SoiTuoiHuyetTrang == null) kq.SoiTuoiHuyetTrang = string.Empty;

                        if (coQuan != CoQuan.Khac && coQuan != CoQuan.KhamPhuKhoa)
                        {
                            if (kq.Normal) workSheet.Shapes[checkIndex].ControlFormat.Value = 1;
                            if (kq.Abnormal) workSheet.Shapes[checkIndex + 1].ControlFormat.Value = 1;
                            workSheet.Cells[string.Format("F{0}", rowIndex)].Value = kq.Note.Replace("\r", "").Replace("\t", "");
                        }
                        else if (coQuan == CoQuan.Khac)
                            workSheet.Cells[string.Format("F{0}", rowIndex)].Value = kq.Note.Replace("\r", "").Replace("\t", "");
                        else
                        {
                            workSheet.Cells[string.Format("F{0}", rowIndex)].Value = string.Format("PARA: {0}", kq.PARA);
                            workSheet.Cells[string.Format("I{0}", rowIndex)].Value = kq.NgayKinhChot.Value.ToString("dd/MM/yyyy");
                            rowIndex++;
                            workSheet.Cells[string.Format("F{0}", rowIndex)].Value = string.Format("Kết quả khám phụ khoa: {0}", kq.Note.Replace("\r", "").Replace("\t", ""));
                            rowIndex++;
                            workSheet.Cells[string.Format("F{0}", rowIndex)].Value = string.Format("Soi tươi huyết trắng: {0}", kq.SoiTuoiHuyetTrang.Replace("\r", "").Replace("\t", ""));
                            if (kq.Normal) workSheet.Shapes[2].ControlFormat.Value = 1;
                            if (kq.Abnormal) workSheet.Shapes[3].ControlFormat.Value = 1;
                        }
                    }

                    checkIndex += 2;
                    rowIndex += 2;
                }

                //Fill thông tin dịch vụ sử dụng lâm sàng
                rowIndex = 37;
                foreach (ServiceHistoryView srvHistory in serviceLamSangList)
                {
                    range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)].EntireRow;
                    range.Insert(InsertShiftDirection.Down);
                    range.Insert(InsertShiftDirection.Down);

                    if (srvHistory.Note == null) srvHistory.Note = string.Empty;
                    string serviceName = string.Empty;
                    if (srvHistory.EnglishName == null || srvHistory.EnglishName.Trim() == string.Empty)
                        serviceName = srvHistory.Name;
                    else
                        serviceName = string.Format("{0} ({1})", srvHistory.EnglishName, srvHistory.Name);

                    range = workSheet.Cells[string.Format("A{0}:E{1}", rowIndex + 1, rowIndex + 2)];
                    range.Merge();
                    range.WrapText = true;
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.Borders.Color = Color.Black;
                    range.Borders.LineStyle = LineStyle.Continuous;
                    range.Borders.Weight = BorderWeight.Thin;
                    range.Font.Bold = true;
                    range.Value = serviceName;

                    shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(0) + 1, workSheet.WindowInfo.RowToPoints(rowIndex) + 15.5, 100, 15);
                    shape.Line.Visible = false;
                    shape.TextFrame.HorizontalAlignment = HAlign.Left;
                    shape.TextFrame.VerticalAlignment = VAlign.Center;

                    if (srvHistory.IsNormalOrNegative)
                    {
                        shape.TextFrame.Characters.Text = "Normal (Bình thường)";
                        shape.ControlFormat.Value = srvHistory.Normal ? 1 : 0;
                    }
                    else
                    {
                        shape.TextFrame.Characters.Text = "Negative (Âm tính)";
                        shape.ControlFormat.Value = srvHistory.Negative ? 1 : 0;
                    }

                    shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(0) + 119, workSheet.WindowInfo.RowToPoints(rowIndex) + 15.5, 110, 15);
                    shape.Line.Visible = false;
                    shape.TextFrame.HorizontalAlignment = HAlign.Left;
                    shape.TextFrame.VerticalAlignment = VAlign.Center;

                    if (srvHistory.IsNormalOrNegative)
                    {
                        shape.TextFrame.Characters.Text = "Abnormal (Bất thường)";
                        shape.ControlFormat.Value = srvHistory.Abnormal ? 1 : 0;
                    }
                    else
                    {
                        shape.TextFrame.Characters.Text = "Positive (Dương tính)";
                        shape.ControlFormat.Value = srvHistory.Positive ? 1 : 0;
                    }

                    range = workSheet.Cells[string.Format("F{0}:I{1}", rowIndex + 1, rowIndex + 2)];
                    range.Merge();
                    range.WrapText = true;
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.Borders.Color = Color.Black;
                    range.Borders.LineStyle = LineStyle.Continuous;
                    range.Borders.Weight = BorderWeight.Thin;
                    range.Value = srvHistory.Note.Replace("\r", "").Replace("\t", "");

                    rowIndex += 2;
                }

                if (rowIndex < 41)
                {
                    int count = 41 - rowIndex;
                    range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)].EntireRow;
                    for (int i = 0; i < count; i++)
                    {
                        range.Insert(InsertShiftDirection.Down);
                    }

                    rowIndex = serviceLamSangList.Count * 2 + 38 + count;
                }
                else
                    rowIndex = serviceLamSangList.Count * 2 + 38;

                //Fill thông tin dịch vụ sử dụng cận lâm sàng
                //rowIndex = serviceLamSangList.Count * 2 + 38;
                range = workSheet.Cells[string.Format("A{0}", rowIndex)].EntireRow;
                range.Insert(InsertShiftDirection.Down);

                range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex)];
                range.Merge();
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;
                range.Font.Bold = true;
                range.Value = "PARACLINICAL RESULTS\nKẾT QUẢ CẬN LÂM SÀNG";
                range.RowHeight = 33.75;

                range = workSheet.Cells[string.Format("F{0}:I{0}", rowIndex)];
                range.Merge();
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;
                range.Font.Bold = true;
                range.Value = "COMMENTS\nNHẬN XÉT";

                List<ServiceHistoryView> serviceNoGroupList = new List<ServiceHistoryView>();
                Hashtable htServiceGroup = new Hashtable();
                foreach (ServiceHistoryView srvHistory in serviceCanLamSangList)
                {
                    result = ServiceGroupBus.GetServiceGroup(srvHistory.ServiceGUID.ToString());
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceGroupBus.GetServiceGroup"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ServiceGroupBus.GetServiceGroup"));
                        return false;
                    }

                    ServiceGroup serviceGroup = result.QueryResult as ServiceGroup;
                    if (serviceGroup == null)
                        serviceNoGroupList.Add(srvHistory);
                    else
                    {
                        if (!htServiceGroup.ContainsKey(serviceGroup.ServiceGroupGUID.ToString()))
                        {
                            List<ServiceHistoryView> serviceGroupList = new List<ServiceHistoryView>();
                            serviceGroupList.Add(srvHistory);
                            htServiceGroup.Add(serviceGroup.ServiceGroupGUID.ToString(), serviceGroupList);
                        }
                        else
                        {
                            List<ServiceHistoryView> serviceGroupList = (List<ServiceHistoryView>)htServiceGroup[serviceGroup.ServiceGroupGUID.ToString()];
                            serviceGroupList.Add(srvHistory);
                        }
                    }
                }

                foreach (List<ServiceHistoryView> serviceGroupList in htServiceGroup.Values)
                {
                    foreach (ServiceHistoryView srvHistory in serviceGroupList)
                    {
                        serviceNoGroupList.Add(srvHistory);
                    }
                }

                foreach (ServiceHistoryView srvHistory in serviceNoGroupList)
                {
                    range = workSheet.Cells[string.Format("A{0}", rowIndex + 2)].EntireRow;
                    range.Insert(InsertShiftDirection.Down);
                    range.Insert(InsertShiftDirection.Down);

                    if (srvHistory.Note == null) srvHistory.Note = string.Empty;
                    string serviceName = string.Empty;
                    if (srvHistory.EnglishName == null || srvHistory.EnglishName.Trim() == string.Empty)
                        serviceName = srvHistory.Name;
                    else
                        serviceName = string.Format("{0} ({1})", srvHistory.EnglishName, srvHistory.Name);

                    range = workSheet.Cells[string.Format("A{0}:E{1}", rowIndex + 1, rowIndex + 2)];
                    range.Merge();
                    range.WrapText = true;
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.Borders.Color = Color.Black;
                    range.Borders.LineStyle = LineStyle.Continuous;
                    range.Borders.Weight = BorderWeight.Thin;
                    range.Font.Bold = true;
                    range.Value = serviceName;

                    shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(0) + 1, workSheet.WindowInfo.RowToPoints(rowIndex) + 15.5, 100, 15);
                    shape.Line.Visible = false;
                    shape.TextFrame.HorizontalAlignment = HAlign.Left;
                    shape.TextFrame.VerticalAlignment = VAlign.Center;

                    if (srvHistory.IsNormalOrNegative)
                    {
                        shape.TextFrame.Characters.Text = "Normal (Bình thường)";
                        shape.ControlFormat.Value = srvHistory.Normal ? 1 : 0;
                    }
                    else
                    {
                        shape.TextFrame.Characters.Text = "Negative (Âm tính)";
                        shape.ControlFormat.Value = srvHistory.Negative ? 1 : 0;
                    }

                    shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(0) + 119, workSheet.WindowInfo.RowToPoints(rowIndex) + 15.5, 110, 15);
                    shape.Line.Visible = false;
                    shape.TextFrame.HorizontalAlignment = HAlign.Left;
                    shape.TextFrame.VerticalAlignment = VAlign.Center;

                    if (srvHistory.IsNormalOrNegative)
                    {
                        shape.TextFrame.Characters.Text = "Abnormal (Bất thường)";
                        shape.ControlFormat.Value = srvHistory.Abnormal ? 1 : 0;
                    }
                    else
                    {
                        shape.TextFrame.Characters.Text = "Positive (Dương tính)";
                        shape.ControlFormat.Value = srvHistory.Positive ? 1 : 0;
                    }

                    range = workSheet.Cells[string.Format("F{0}:I{1}", rowIndex + 1, rowIndex + 2)];
                    range.Merge();
                    range.WrapText = true;
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.Borders.Color = Color.Black;
                    range.Borders.LineStyle = LineStyle.Continuous;
                    range.Borders.Weight = BorderWeight.Thin;
                    range.Value = srvHistory.Note.Replace("\r", "").Replace("\t", "");

                    rowIndex += 2;
                }

                range = workSheet.Cells[string.Format("A{0}", rowIndex + 2)].EntireRow;
                range.Insert(InsertShiftDirection.Down);
                range.Insert(InsertShiftDirection.Down);
                
                range = workSheet.Cells[string.Format("A{0}:E{1}", rowIndex + 1, rowIndex + 2)];
                range.Merge();
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.Left;
                range.VerticalAlignment = VAlign.Center;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;
                range.Font.Bold = true;
                range.Value = "Others (Cận lâm sàng khác)";

                range = workSheet.Cells[string.Format("F{0}:I{1}", rowIndex + 1, rowIndex + 2)];
                range.Merge();
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.Left;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                rowIndex += 3;

                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)].EntireRow;
                range.Insert(InsertShiftDirection.Down);
                range.Insert(InsertShiftDirection.Down);
                range.Insert(InsertShiftDirection.Down);
                range.Insert(InsertShiftDirection.Down);
                range.Insert(InsertShiftDirection.Down);

                range = workSheet.Cells[string.Format("A{0}", rowIndex)];
                range.Font.Bold = true;
                range.Value = "1. CÁC XÉT NGHIỆM LÀM THÊM";

                shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(5) + 25, workSheet.WindowInfo.RowToPoints(rowIndex - 1), 40, 15);
                shape.Line.Visible = false;
                shape.TextFrame.HorizontalAlignment = HAlign.Left;
                shape.TextFrame.VerticalAlignment = VAlign.Center;
                shape.TextFrame.Characters.Text = "Có";

                shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(6) + 15, workSheet.WindowInfo.RowToPoints(rowIndex - 1), 50, 15);
                shape.Line.Visible = false;
                shape.TextFrame.HorizontalAlignment = HAlign.Left;
                shape.TextFrame.VerticalAlignment = VAlign.Center;
                shape.TextFrame.Characters.Text = "Không";

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}:I{0}", rowIndex)];
                range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;
                range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dot;
                range.Borders[BordersIndex.EdgeBottom].Weight = BorderWeight.Thin;

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}:I{0}", rowIndex)];
                range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;
                range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dot;
                range.Borders[BordersIndex.EdgeBottom].Weight = BorderWeight.Thin;

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex)];
                range.Font.Bold = true;
                range.Value = "2. ĐÃ LÀM ĐỦ CẬN LÂM SÀNG TRONG GÓI KHÁM";

                shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(5) + 25, workSheet.WindowInfo.RowToPoints(rowIndex - 1), 40, 15);
                shape.Line.Visible = false;
                shape.TextFrame.HorizontalAlignment = HAlign.Left;
                shape.TextFrame.VerticalAlignment = VAlign.Center;
                shape.TextFrame.Characters.Text = "Có";

                shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(6) + 15, workSheet.WindowInfo.RowToPoints(rowIndex - 1), 50, 15);
                shape.Line.Visible = false;
                shape.TextFrame.HorizontalAlignment = HAlign.Left;
                shape.TextFrame.VerticalAlignment = VAlign.Center;
                shape.TextFrame.Characters.Text = "Không";

                range = workSheet.Cells[string.Format("H{0}", rowIndex)];
                range.Value = "        Lý do:………….";

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex)];
                range.Font.Bold = true;
                range.Value = "3. ĐỦ SỨC KHỎE LÀM VIỆC";

                shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(5) + 25, workSheet.WindowInfo.RowToPoints(rowIndex - 1), 40, 15);
                shape.Line.Visible = false;
                shape.TextFrame.HorizontalAlignment = HAlign.Left;
                shape.TextFrame.VerticalAlignment = VAlign.Center;
                shape.TextFrame.Characters.Text = "Có";

                shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(6) + 15, workSheet.WindowInfo.RowToPoints(rowIndex - 1), 50, 15);
                shape.Line.Visible = false;
                shape.TextFrame.HorizontalAlignment = HAlign.Left;
                shape.TextFrame.VerticalAlignment = VAlign.Center;
                shape.TextFrame.Characters.Text = "Không";

                range = workSheet.Cells[string.Format("H{0}", rowIndex)];
                range.Value = "        Lý do:………….";

                
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)].EntireRow;
                for (int i = 0; i < 16; i++)
                {
                    range.Insert(InsertShiftDirection.Down);
                }

                rowIndex += 16;

                //Fill thông tin lời khuyên
                if (loiKhuyenList.Count > 0)
                {
                    rowIndex += 2;
                    range = workSheet.Cells[string.Format("A{0}", rowIndex)].EntireRow;
                    range.Insert(InsertShiftDirection.Down);
                    range.Insert(InsertShiftDirection.Down);

                    range = workSheet.Cells[string.Format("A{0}", rowIndex)];
                    range.Value = "ĐỀ NGHỊ THEO DÕI THÊM";
                    range.Font.Bold = true;
                    range.Font.Underline = UnderlineStyle.Single;

                    rowIndex += 1;
                    range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)].EntireRow;
                    range.Insert(InsertShiftDirection.Down);

                    foreach (LoiKhuyenView loiKhuyen in loiKhuyenList)
                    {
                        string symptomName = loiKhuyen.SymptomName.Replace("\r", "").Replace("\t", "");
                        string advice = loiKhuyen.Advice.Replace("\r", "").Replace("\t", "");
                        range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                        range.Merge();
                        range.WrapText = true;
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Center;
                        range.Borders.Color = Color.Black;
                        range.Borders.LineStyle = LineStyle.Continuous;
                        range.Borders.Weight = BorderWeight.Thin;
                        range.Value = symptomName;

                        range = workSheet.Cells[string.Format("F{0}:I{0}", rowIndex + 1)];
                        range.Merge();
                        range.WrapText = true;
                        range.WrapText = true;
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Borders.Color = Color.Black;
                        range.Borders.LineStyle = LineStyle.Continuous;
                        range.Borders.Weight = BorderWeight.Thin;
                        range.Value = advice;

                        int lineCount = advice.Length / 45;
                        if (lineCount % 40 != 0) lineCount++;

                        range.RowHeight = 15.75 * lineCount;
                        rowIndex += 1;
                        range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)].EntireRow;
                        range.Insert(InsertShiftDirection.Down);
                        range.RowHeight = 15.75;
                    }
                }

                rowIndex += 7;
                range = workSheet.Cells[string.Format("G{0}", rowIndex)];
                range.Value = string.Format("Ngày: {0}", DateTime.Now.ToString("dd/MM/yyyy"));

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }
    }
}
