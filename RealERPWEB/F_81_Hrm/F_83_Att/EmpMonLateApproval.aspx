<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpMonLateApproval.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.EmpMonLateApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

    
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function openModal() {
            //    $('#myModal').modal('show');
            // alert("Hello");
            $('#myModal').modal('toggle');
        }
        function CloseModal() {

            $('#myModal').modal('hide');
        }

        function Openearleavemodal() {
            $('#earleavemodal').modal('toggle');
        }

        function CloseEar() {
            $('#earleavemodal').modal('hide');
        }      
        function openModalAbs() {
            //    $('#myModal').modal('show');
            // alert("Hello");
            $('#absmodal').modal('toggle');
        }


        function CloseModalAbs() {

            $('#absmodal').modal('hide');
        }

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });


            var gvAllLate = $('#<%=this.grvAdjDay.ClientID %>');
            gvAllLate.Scrollable();


            var gvAllLp = $('#<%=this.gvmapsapp.ClientID %>');
            gvAllLp.Scrollable();


            var gvabsapp02 = $('#<%=this.gvabsapp02.ClientID %>');
            gvabsapp02.Scrollable();

            var gvOPunch = $('#<%=this.gvOPunch.ClientID %>');
            gvOPunch.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });

            $('.select2').each(function () {
                var select = $(this);
                select.select2({
                    placeholder: 'Select an option',
                    width: '100%',
                    allowClear: !select.prop('required'),
                    language: {
                        noResults: function () {
                            return "{{ __('No results found') }}";
                        }
                    }
                });
            });
        }



        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {
                        inputList[i].checked = true;

                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                        }
                        else {
                            //row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }

                }

            }

        }

    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="card-fluid container-data  mt-5">
                <div class="col-12 col-lg-12 col-xl-12">
                    <section class="card card-fluid" style="min-height: 650px">
                        <!-- .card-body -->
                        <header class="card-header">
                            <div class="row from-row">
                                <div class="col-md-3">
                                    <div class="input-group input-group-alt ">
                                        <div class="input-group-prepend">
                                            <button class="btn btn-secondary ml-1" type="button">Date </button>
                                        </div>
                                        <asp:TextBox ID="txtfrmDate" runat="server" CssClass=" form-control "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>

                                        <div class="input-group-prepend">
                                            <button class="btn btn-secondary ml-1" type="button">To </button>
                                        </div>

                                        <asp:TextBox ID="txttoDate" runat="server" CssClass=" form-control "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>

                                    </div>
                                </div>

                                <asp:LinkButton ID="ibtnFindCompany" runat="server" CssClass="col-md-1 col-form-label" OnClick="ibtnFindCompany_Click">
                                                  Company</asp:LinkButton>
                                <div class="col-md-2">

                                    <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="chzn-select form-control" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                    </asp:DropDownList>
                                </div>
                                <asp:LinkButton ID="imgbtnDeptSrch" runat="server" CssClass="col-md-1 col-form-label" OnClick="imgbtnDeptSrch_Click">
                                                  Department</asp:LinkButton>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="chzn-select form-control" AutoPostBack="true" TabIndex="2">
                                    </asp:DropDownList>
                                </div>
                                <asp:LinkButton ID="imgbtnSection" runat="server" CssClass="col-md-1 col-form-label" OnClick="imgbtnSection_Click">
                                                  Section</asp:LinkButton>
                                <div class="col-md-2">

                                    <asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control select2" SelectionMode="Multiple"></asp:ListBox>


                                </div>
                                <%-- <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" CssClass="dataLblview form-control"
                                        MaxDropDownHeight="200" TabIndex="8" TransitionalMode="True" >
                                    </cc1:DropCheck>--%>
                            </div>
                            <div class="row from-row mt-1">
                                <div class="col-md-2">
                                    <div class="input-group input-group-alt ">
                                        <div class="input-group-prepend">
                                            <button class="btn btn-secondary ml-1" type="button">Page Size </button>
                                        </div>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem Selected="True">300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="input-group input-group-alt ">
                                        <div class="input-group-prepend">

                                            <asp:LinkButton ID="imgbtnSearchEmployees" runat="server" CssClass="btn btn-secondary">
                                                  Card #</asp:LinkButton>
                                        </div>
                                        <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="input-group-prepend">

                                            <asp:LinkButton ID="imgbtnSearchEmployee" runat="server" CssClass="btn btn-md btn-primary" OnClick="imgbtnSearchEmployee_Click"><i class="fas fa-search "></i></asp:LinkButton>


                                        </div>
                                    </div>


                                </div>
                                <div class="col-md-2">
                                    <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn " OnClick="lnkbtnShow_Click" Text="Ok"></asp:LinkButton>

                                </div>
                                <div class="col-md-6">
                                </div>

                            </div>
                            <div class="form-group" id="Pnldesig" runat="server" visible="false">
                                <div class="col-md-3 pading5px asitCol2">
                                    <asp:Label ID="lblfrmDesig" runat="server" CssClass="lblTxt lblName">Form</asp:Label>
                                    <asp:DropDownList ID="ddlfrmDesig" runat="server" Width="100" OnSelectedIndexChanged="ddlfrmDesig_SelectedIndexChanged" AutoPostBack="true" CssClass="chzn-select form-control inputTxt" TabIndex="6">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-4 pading5px">
                                    <asp:Label ID="lbltoDesig" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                    <asp:DropDownList ID="ddlToDesig" runat="server" Width="100" CssClass="chzn-select form-control inputTxt" TabIndex="6">
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </header>
                        <div class="card-body">
                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="ViewMonLateApproval" runat="server">
                                    <div class="row">
                                        <asp:GridView ID="grvAdjDay" runat="server"
                                            AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" OnSelectedIndexChanged="grvAdjDay_SelectedIndexChanged">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo8" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvEmpIdAdj" runat="server" Font-Bold="True" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCompany" runat="server" Font-Bold="true" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Companyname")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section">


                                                    <HeaderTemplate>
                                                        <table style="width: 47%;">
                                                            <tr>
                                                                <td class="style58">
                                                                    <asp:Label ID="Label4Adj" runat="server" Font-Bold="True"
                                                                        Text="Section" Width="180px"></asp:Label>
                                                                </td>
                                                                <td class="style60">&nbsp;</td>
                                                                <td>
                                                                    <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>



                                                    <ItemTemplate>
                                                        <asp:LinkButton OnClick="lblgvdeptandemployeeemp_Click" ID="lblgvdeptandemployeeemp" runat="server"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                            Width="250px"></asp:LinkButton>


                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Card #">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnTotalDay" runat="server" OnClick="lbtnTotalDay_Click"
                                                            CssClass="btn btn-sm  btn-primary primarygrdBtn">Total</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCardnoearn" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>

                                                        <asp:Label ID="emdname" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                            Width="120px"></asp:Label>
                                                        <asp:Label ID="lblgvEmpNameearn" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnUpdateDayAdj" runat="server" OnClick="btnUpdateDayAdj_Click"
                                                            CssClass="btn   btn-danger btn-sm primarygrdBtn">Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Available <br> CL" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbalclv" runat="server" Height="16px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balclv")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Available <br> EL" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbalernlv" runat="server" Height="16px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balernlv")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Total <br> Late Days" ItemStyle-BackColor="#dff0d8 " ItemStyle-ForeColor="#000">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtLateday" runat="server" BackColor="Transparent" AutoPostBack="true"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:TextBox>


                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFDelday" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Informed <br>  Admin Dept.">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnCalCulationSadj" runat="server" Font-Bold="True"
                                                            Font-Size="12px" OnClick="lbtnCalCulationSadj_Click"
                                                            CssClass="btn  btn-sm  btn-primary primarygrdBtn">Calculation</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtaprday" runat="server" BackColor="Transparent" OnTextChanged="txtaprday_TextChanged"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right" AutoPostBack="true"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LWP Days">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAdj" runat="server" BackColor="Transparent" OnTextChanged="txtAdj_TextChanged"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right" AutoPostBack="true"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAdj" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Leave Adj CL" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtlvAdj" runat="server" BackColor="Transparent" OnTextChanged="txtlvAdj_TextChanged"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right" AutoPostBack="true"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leaveadj")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFlvAdj" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Adj EL" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEllvAdj" runat="server" BackColor="Transparent" OnTextChanged="txtEllvAdj_TextChanged"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right" AutoPostBack="true"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leaveadjel")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFEllvAdj" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Leave  Adjust" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvttdelv" runat="server" Height="16px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttdelv")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="is Adjust?" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="isAdjust" runat="server"
                                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isadjust"))=="True" %>'
                                                            Width="50px" />


                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>

                                <asp:View ID="ViewOnetimePunch" runat="server">
                                    <div class="row">
                                        <asp:GridView ID="gvOPunch" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" ShowFooter="True">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvEmpIdP" runat="server" Font-Bold="True" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCompanyp" runat="server" Font-Bold="true" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Companyname")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdeptandemployeeempp" runat="server"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                            Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Card #">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnTotalP" runat="server" OnClick="lbtnTotalP_Click"
                                                            CssClass="btn btn-sm  btn-primary primarygrdBtn">Total</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCardnoearnp" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvEmpNameearnp" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnUpdatePunch" runat="server"
                                                            OnClick="btnUpdatePunch_Click"
                                                            CssClass="btn btn-sm  btn-danger primarygrdBtn">Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total  Days">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPunchDay" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opunchday")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Informed Admin Dept.">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnCalCulationPday" runat="server" OnClick="lbtnCalCulationPday_Click"
                                                            CssClass="btn btn-sm  btn-primary  primarygrdBtn">Calculation</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtpaprday" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paprday")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deduction Days">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtpAdj" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pdedday")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="ViewMonaBSApproval" runat="server">
                                    <asp:GridView ID="gvmapsapp" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo9abs" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpIdabs" runat="server" Font-Bold="True" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="180px"></asp:Label>

                                                    <asp:Label ID="emdname" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                        Width="120px"></asp:Label>


                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCompanyabs" runat="server" Font-Bold="true" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Companyname")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>


                                                    <asp:LinkButton OnClick="lnkbtnAbsAppGVmapsapp_Click" ID="lnkbtnAbsAppGVmapsapp" runat="server"
                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                        Width="250px"></asp:LinkButton>





                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Card #">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalabs" runat="server" OnClick="lbtnTotalabs_Click"
                                                        CssClass="btn btn-sm   btn-primary primarygrdBtn">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardnoearnabs" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpNameearnabs" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="btnUpdateAbsent" runat="server"
                                                        OnClick="btnUpdateAbsent_Click"
                                                        CssClass="btn btn-sm  btn-danger primarygrdBtn">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total  Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvabsday" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Informed Admin Dept.">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtabsaprday" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deduction Days">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtabsAdj" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>



                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Leave adj">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtabslvadj" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leaveadj")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Balance">
                                                <ItemTemplate>


                                                    <asp:Label ID="txbalancelvadj" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balance")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Reason">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtabsreason" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reason")) %>'
                                                        Width="120px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>

                                </asp:View>

                                <asp:View ID="EarlyLeave" runat="server">
                                    <div class="row">
                                        <asp:GridView ID="gvEarlyleave" runat="server"
                                            AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" OnSelectedIndexChanged="gvEarlyleave_SelectedIndexChanged">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo20" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvEarlyempid" runat="server" Font-Bold="True" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvEarlyCompany" runat="server" Font-Bold="true" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Companyname")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section">


                                                    <HeaderTemplate>
                                                        <table style="width: 47%;">
                                                            <tr>
                                                                <td class="style58">
                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="Section" Width="180px"></asp:Label>
                                                                </td>
                                                                <td class="style60">&nbsp;</td>
                                                                <td>
                                                                    <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>



                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgvEarlydept" OnClick="lblgvEarlydept_OnClick" runat="server"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                            Width="250px"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Card #">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnTotalELeave" runat="server" OnClick="lbtnTotalELeave_Click"
                                                            CssClass="btn   btn-primary primarygrdBtn">Total</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvEarlyCard" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvEarlyDesig" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnUpdateEarly" runat="server" OnClick="btnUpdateEarly_Click"
                                                            CssClass="btn   btn-danger primarygrdBtn">Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Early Leave Days">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtEarlyday" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delday")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>

                                                        <%--<asp:Label ID="lblgvDelday" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFEarly" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Early Leave Approv.">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnCalEarly" runat="server" Font-Bold="True"
                                                            Font-Size="12px" OnClick="lbtnCalEarly_Click"
                                                            CssClass="btn    btn-primary primarygrdBtn">Calculation</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtInformDay" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprday")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Not Approved">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAdjustday" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedday")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAdj11" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="ViewAbsenApp02" runat="server">


                                    <asp:GridView ID="gvabsapp02" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo9abs02" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpIdabs02" runat="server" Font-Bold="True" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCompanyabs02" runat="server" Font-Bold="true" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Companyname")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Width="100px"
                                                        Text="Section">
                                                        <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                            CssClass="btn btn-success ml-2 btn-xs" ToolTip="Export Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                    </asp:Label>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardnoearnabs" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="100px"></asp:Label>


                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Card #">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardnoearnabss" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name">

                                                <ItemTemplate>
                                                    <asp:LinkButton ID="hlnkgvdeptandemployeeempabs02" runat="server" OnClick="hlnkgvdeptandemployeeempabs02_Click"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                        Width="250px"></asp:LinkButton>
                                                    <%--Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+ --%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpNameearnabs02" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total  Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvabsday02" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Informed Admin Dept.">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnUpdateMonthAbsDay" runat="server" Font-Bold="True"
                                                        Font-Size="12px" OnClick="lbtnUpdateMonthAbsDay_Click"
                                                        CssClass="btn btn-sm   btn-primary primarygrdBtn">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtabsaprdaylp" runat="server" BackColor="Transparent" AutoPostBack="true"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right" OnTextChanged="txtabsaprdaylp_TextChanged"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absapp")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance Day">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvabsdayApp" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>

                                </asp:View>

                                <asp:View ID="LPAproval" runat="server">
                                    <div class="row">
                                        <asp:GridView ID="gvLPAproval" runat="server"
                                            AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" OnSelectedIndexChanged="grvAdjDay_SelectedIndexChanged">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo8lp" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvEmpIdAdjlp" runat="server" Font-Bold="True" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCompanylp" runat="server" Font-Bold="true" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Companyname")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section">


                                                    <HeaderTemplate>
                                                        <table style="width: 47%;">
                                                            <tr>
                                                                <td class="style58">
                                                                    <asp:Label ID="Label4Adjlp" runat="server" Font-Bold="True"
                                                                        Text="Section" Width="180px"></asp:Label>
                                                                </td>
                                                                <td class="style60">&nbsp;</td>
                                                                <td>
                                                                    <asp:HyperLink ID="hlbtntbCdataExellp" runat="server" BackColor="#000066"
                                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>



                                                    <ItemTemplate>
                                                        <asp:LinkButton OnClick="lblgvdeptandemployeeempLP_Click" ID="lblgvdeptandemployeeempLP" runat="server"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                            Width="250px"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Card #">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnTotalDaylp" runat="server" OnClick="lbtnTotalDay_Click"
                                                            CssClass="btn btn-sm  btn-primary primarygrdBtn">Total</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCardnoearnlp" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvEmpNameearnlp" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnUpdateDayAdjlp" runat="server" OnClick="btnUpdateDayAdjlp_Click"
                                                            CssClass="btn  btn-sm btn-danger primarygrdBtn">Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Late Days">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtLatedaylp" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delday")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>

                                                        <%--<asp:Label ID="lblgvDelday" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFDelday" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Informed Admin Dept.">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnCalCulationSadjLP" runat="server" Font-Bold="True"
                                                            Font-Size="12px" OnClick="lbtnCalCulationSadjLP_Click"
                                                            CssClass="btn    btn-primary primarygrdBtn">Calculation</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtaprdaylp" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprday")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Deduction Days">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAdjlp" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAdjlp" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Leave adj">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtlvAdjlp" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leaveadj")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFlvAdjlp" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reason">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtlpreason" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reason")) %>'
                                                            Width="120px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFlreason" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>


                            </asp:MultiView>
                        </div>
                    </section>
                </div>
            </div>


            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A d-none">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>

                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">To</asp:Label>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <div class="colMdbtn pading5px">
                                        </div>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcDepartment" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnDseptSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnDeptSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="xddlDepartment" runat="server" Width="335" CssClass="chzn-select form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblSection" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                        <asp:TextBox ID="txtSrcSection" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ximgbtnSection" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSection_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>




                                </div>




                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                    </div>

                </div>
            </div>
            <!------ modal---------------->
            <div id="myModal" class="modal fade  " role="dialog" data-keyboard="false" data-backdrop="static">
<%--            <div class="modal fade bd-example-modal-lg" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">--%>
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLongTitle">
                                <asp:Label ID="lbmodalheading" runat="server"></asp:Label></h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>

                        </div>
                        <div class="modal-body">
                            <p id="EmpDeatials" class="m-0" runat="server">Name</p>
                            <p id="DeatialsDate" runat="server">Date</p>
                            <asp:GridView ID="mgvbreakdown" runat="server"
                                AutoGenerateColumns="False" CssClass="mt-2 table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="false">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="mlblgvSlNo8" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="mlgvEmpIdAdj" runat="server" Font-Bold="True" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="mlblgvCompany" runat="server" Font-Bold="true" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section" Visible="false">


                                        <HeaderTemplate>
                                            <table style="width: 30%;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="mLabel14" Font-Size="Smaller" runat="server" Font-Bold="True"
                                                            Text="Name" Width="70px"></asp:Label>
                                                    </td>
                                                    <td class="style60">&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="mhlbtntbCdataExel" runat="server" BackColor="#000066"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            ForeColor="White" Style="text-align: center" Width="50px"><span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>



                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdeptandemployeeempm" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))  %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Card #">
                                        <FooterTemplate>
                                            <%-- <asp:LinkButton ID="mlbtnTotalDay" runat="server" OnClick="lbtnTotalDay_Click"
                                                     CssClass="btn   btn-primary primarygrdBtn">Total</asp:LinkButton>--%>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="mlblgvCardnoearn" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="mlblgvEmpNameearn" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <%-- <FooterTemplate>
                                                <asp:LinkButton ID="mbtnUpdateDayAdj" runat="server" OnClick="btnUpdateDayAdj_Click"
                                                     CssClass="btn   btn-danger primarygrdBtn">Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Late Date">

                                        <ItemTemplate>
                                            <asp:Label ID="mlblgvlateday" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="mlgvFDelday14" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="In Time">

                                        <ItemTemplate>
                                            <asp:Label ID="mlblgvStdntime" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("hh:mm tt") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual IN">

                                        <ItemTemplate>
                                            <asp:Label ID="mlblgvactul" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Late(Grace Inc)">

                                        <ItemTemplate>
                                            <asp:Label ID="mlblgvlatemin" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "latemin")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">

                                        <ItemTemplate>
                                            <asp:TextBox ID="mTxtremarks" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 20px;">
                                                <tr>
                                                    <td>Inform?
                                                  <%-- <asp:CheckBox ID="chkall" runat="server" 
                                                        OnCheckedChanged="chkall_CheckedChanged" AutoPostBack="true" /></td>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkack" runat="server"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lateapp"))=="True" ? true : false %>'
                                                Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lateapp"))=="True" ? false : true%>'
                                                Width="20px" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="ModalUpdateBtn" runat="server" OnClick="ModalUpdateBtn_Click" OnClientClick="CloseModal();"
                                CssClass="btn btn-sm btn-primary"> <span class="glyphicon glyphicon-saved"></span> Update</asp:LinkButton>

                            <asp:LinkButton ID="ModallnkBtnLateAFTER10AM" data-dismiss="modal" OnClientClick="CloseModal();" OnClick="ModallnkBtnLateAFTER10AM_Click" Visible="false"
                                runat="server" CssClass="btn btn-sm btn-primary"> <span class="glyphicon glyphicon-saved"></span> Update</asp:LinkButton>

                            <button type="button" class="btn btn-sm btn-warning" data-dismiss="modal">Close</button>
                        </div>

                    </div>
                </div>
            </div>


            <div id="earleavemodal" class="modal animated zoomIn" role="dialog">
                <div class="modal-dialog   modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">

                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title">
                                <span class="glyphicon glyphicon-hand-right"></span>
                                <asp:Label ID="lblearlylvapp" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">

                            <div class="row-fluid form-horizontal forgotform" id="">
                            </div>
                            <div class="">
                                <asp:GridView ID="gvearleave" runat="server"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="572px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvSlNo8p" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="mlgvEmpIdAdjp" runat="server" Font-Bold="True" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvCompanyp" runat="server" Font-Bold="true" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">


                                            <HeaderTemplate>
                                                <table style="width: 30%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="mLabel4" Font-Size="Smaller" runat="server" Font-Bold="True"
                                                                Text="Name" Width="70px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="mhlbtntbCdataExelp" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="50px"><span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>



                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdeptandemployeeempmp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))  %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <FooterTemplate>
                                                <%-- <asp:LinkButton ID="mlbtnTotalDay" runat="server" OnClick="lbtnTotalDay_Click"
                                                     CssClass="btn   btn-primary primarygrdBtn">Total</asp:LinkButton>--%>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvCardnoearnp" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvEmpNameearnp" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                <asp:LinkButton ID="mbtnUpdateDayAdj" runat="server" OnClick="btnUpdateDayAdj_Click"
                                                     CssClass="btn   btn-danger primarygrdBtn">Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Early Leave Date">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvlatedayp" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="mlgvFDelday" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="In Time">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvStdntimep" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")).ToString("hh:mm tt") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual IN">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvactulp" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Late">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvlateminp" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "latemin")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">

                                            <ItemTemplate>
                                                <asp:TextBox ID="mTxtremarksp" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table style="width: 20px;">
                                                    <tr>
                                                        <td>Inform?
                                                  <%-- <asp:CheckBox ID="chkall" runat="server" 
                                                        OnCheckedChanged="chkall_CheckedChanged" AutoPostBack="true" /></td>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkack" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "earleaveapp"))=="True" ? true : false %>'
                                                    Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "earleaveapp"))=="True" ? false : true%>'
                                                    Width="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>




                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="lnkearapp" OnClientClick="CloseEar();" OnClick="lnkearapp_OnClick"
                                runat="server" CssClass="btn btn-sm btn-primary"> <span class="glyphicon glyphicon-saved"></span> Update</asp:LinkButton>
                            <button type="button" class="btn btn-sm btn-warning" data-dismiss="modal">Close</button>






                        </div>
                    </div>
                </div>
            </div>


            <div id="absmodal" class="modal animated zoomIn" role="dialog">
                <div class="modal-dialog   modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <button type="button" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title">
                                <span class="glyphicon glyphicon-hand-right"></span>
                                <asp:Label ID="lblabsheading" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">


                            <div class="">
                                <asp:GridView ID="mgvmonabsent" runat="server"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="572px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvSlNoabs02" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="mlgvEmpIdabs02" runat="server" Font-Bold="True" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvCompanyabs02" runat="server" Font-Bold="true" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">


                                            <HeaderTemplate>
                                                Name
                                                     <asp:HyperLink ID="mhlbtntbCdataExelabs02" runat="server"
                                                         ForeColor="White" Style="text-align: center" Width="50px"><span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                            </HeaderTemplate>



                                            <ItemTemplate>
                                                <asp:Label ID="lblgvempnameabs02" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))  %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <FooterTemplate>
                                                <%-- <asp:LinkButton ID="mlbtnTotalDay" runat="server" OnClick="lbtnTotalDay_Click"
                                                     CssClass="btn   btn-primary primarygrdBtn">Total</asp:LinkButton>--%>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvCardnoabs02" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvdesigabs02" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                <asp:LinkButton ID="mbtnUpdateDayAdj" runat="server" OnClick="btnUpdateDayAdj_Click"
                                                     CssClass="btn   btn-danger primarygrdBtn">Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Absent Date">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvabsday" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="mlgvFDelday" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">

                                            <ItemTemplate>
                                                <asp:TextBox ID="lblgvremarks" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Inform?
                                                   
                                                          <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />



                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="lblchkaabs02" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "absapp"))=="True" ? true : false %>'
                                                    Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isApproved"))=="True" ? false : true %>'
                                                    Width="20px" />

                                                <asp:CheckBox ID="isApproved" runat="server" Visible="false"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isApproved"))=="True" ? true : false %>'
                                                    Width="20px" />

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="lbntnAbsentApproval" OnClientClick="CloseModalAbs();" OnClick="lbntnAbsentApproval_Click"
                                runat="server" CssClass="btn btn-sm btn-primary"> <span class="glyphicon glyphicon-saved"></span> Update</asp:LinkButton>
                            <button type="button" class="btn btn-sm btn-warning" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

