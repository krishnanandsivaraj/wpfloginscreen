using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace LMWPilot.Common
{

    public partial class GeneralContent : UserControl
    {
        private static HSSFWorkbook hssfworkbook;

        public GeneralContent()
        {
            InitializeComponent();
        }

        public static int LoadImage(string path, HSSFWorkbook wb)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);
            return wb.AddPicture(buffer, PictureType.JPEG);
        }

        private static void WriteToFile()
        {
            FileStream file = new FileStream(@"LMW_" + Environment.UserName + ".xls", FileMode.Open);
            hssfworkbook.Write(file);
            file.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitializeWorkbook();

            Sheet sheet1 = hssfworkbook.CreateSheet("Cover Page");
            //sheet1.GetRow(8).GetCell(3).SetCellValue(customerNameTBox.Text.ToString());
            //sheet1.GetRow(22).GetCell(1).SetCellValue(machineNameTBox.Text.ToString());
            //sheet1.GetRow(22).GetCell(3).SetCellValue(componentTBox.Text.ToString());
            //sheet1.GetRow(22).GetCell(4).SetCellValue(drawingNumberTBox.Text.ToString());
            //sheet1.GetRow(22).GetCell(7).SetCellValue(cycleTimeTBox.Text.ToString());
            //sheet1.GetRow(26).GetCell(3).SetCellValue(preparedbyTBox.Text.ToString());
            //sheet1.GetRow(26).GetCell(7).SetCellValue(dateTBox.Text.ToString());
            HSSFPatriarch patriarch = (HSSFPatriarch)sheet1.CreateDrawingPatriarch();
            HSSFClientAnchor anchor;
            anchor = new HSSFClientAnchor(1, 2, 1, 25, 2, 10, 2, 10);
            anchor.AnchorType = 2;
            HSSFPicture picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage(@"..\..\images\"+UploadTBox.Text+".jpg", hssfworkbook));
            picture.Resize();
            picture.LineStyle = HSSFPicture.LINESTYLE_DASHDOTGEL;

            WriteToFile();
            MessageBox.Show("File Created successfully!!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private void InitializeWorkbook()
        {
            hssfworkbook = new HSSFWorkbook();

            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "LMW Coimbatore";
            hssfworkbook.DocumentSummaryInformation = dsi;

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "LMW Timesheet Automation System (T-SAS)";
            hssfworkbook.SummaryInformation = si;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
             string filename = UploadTBox.Text;
            if (File.Exists(filename))
            {
                // TODO: Show an error message box to user indicating destination file already uploaded
                return;
            }

            string name = Path.GetFileName(filename);
            string destinationFilename = Path.Combine(@"C:\uploadfile", name);

            File.Copy(filename, destinationFilename);
        }
    }
}