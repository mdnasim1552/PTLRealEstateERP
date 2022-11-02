<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptAttendenceSheet.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.RptAttendenceSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .modalcss {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            position: fixed;
            overflow: scroll;
        }

        .multiselect {
            width: 233px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 200px !important;
        }

        .multiselect-container {
            height: 250px !important;
            width: 250px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 200px !important;
        }

        .rbtnAtten tbody tr td {
            margin: 0 5px;
        }

            .rbtnAtten tbody tr td input[type=checkbox], .rbtnAtten tbody tr td input[type=radio] {
                box-sizing: border-box;
                padding: 0;
                margin: 0 0 0 12px;
            }

            .rbtnAtten tbody tr td label {
                margin: 0 0 0 5px;
            }
    </style>


    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $(".select2").select2();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });


          <%-- var gvMonthlyattSummary = $('#<%=this.gvMonthlyattSummary.ClientID %>');
            gvMonthlyattSummary.Scrollable();--%>

            var gvemplateatt = $('#<%=this.gvemplateatt.ClientID %>');
            gvemplateatt.Scrollable();

            var gvEmpStatus = $('#<%=this.gvEmpStatus.ClientID %>');
            gvEmpStatus.Scrollable();


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
        function Search_Gridview(strKey) {

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvMonthlyattSummary.ClientID %>");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {

                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
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

            <div class="card card-fluid mt-5" style="min-height: 550px;">
                <div class="card-header mb-2">
                    <div class="row mb-2">
                        <div class="col-12">

                            <asp:RadioButtonList ID="rbtnAtten" runat="server" AutoPostBack="True"
                                BackColor="#DFF0D8" Style="border-radius: 5px; padding: 0 5px;" BorderColor="#000" CssClass="custom-control custom-control-inline custom-checkbox rbtnAtten"
                                Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                OnSelectedIndexChanged="rbtnAtten_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem>Atten.Log</asp:ListItem>
                                <asp:ListItem>Daily Attendance</asp:ListItem>
                                <asp:ListItem>Emp.status</asp:ListItem>
                                <asp:ListItem>Monthly Attentdance</asp:ListItem>
                                <asp:ListItem>Monthly Late Attendance</asp:ListItem>
                                <asp:ListItem>Emp. status(Late)</asp:ListItem>
                                <asp:ListItem>Early Leave</asp:ListItem>
                                <asp:ListItem>Early Leave(Daily)</asp:ListItem>
                                <asp:ListItem>Late (Daily)</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Company  
                                </label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Department  
                                </label>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="col-md-3" id="PnlSection" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Section  
                                </label>
                                <asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control select2" SelectionMode="Multiple" OnSelectedIndexChanged="DropCheck1_SelectedIndexChanged"></asp:ListBox>
                            </div>
                        </div>

                        <div class="col-md-3" id="empListPnl" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType" class="d-block">
                                    Emp.Name 
                                    <asp:LinkButton ID="lnkbtnEmp" runat="server" OnClick="lnkbtnEmp_Click">  <i class="fa fa-search"> </i> </asp:LinkButton>
                                    <span class="float-right isFormulaChekcboxdv">
                                        <asp:CheckBox ID="isResignChekcbox" runat="server" AutoPostBack="True" ForeColor="red" Text="Resign" OnCheckedChanged="isResignChekcbox_CheckedChanged" CssClass="margin:0" /></span>
                                </label>

                                <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control select2" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>



                    </div>


                    <div class="row">



                        <div class="col-md-2" id="Div1" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server">Form</asp:Label>



                                <asp:TextBox ID="txtfromdate" runat="server" CssClass=" form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2" id="Div2" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lbltodate" runat="server">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass=" form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                            </div>
                        </div>



                        <div class="col-2" id="isStatusType" runat="server">
                            <asp:Label ID="Label1" runat="server">Status Type</asp:Label>

                            <asp:RadioButtonList ID="rbtnAttStatus" runat="server" AutoPostBack="True"
                                CssClass="custom-control custom-checkbox d-block p-0 mt-2"
                                Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                RepeatDirection="Horizontal">
                                <asp:ListItem>Time wise</asp:ListItem>
                                <asp:ListItem>Att Status</asp:ListItem>

                            </asp:RadioButtonList>

                        </div>
                        <div id="pnlDesig" runat="server" visible="false" class="col-md-4">
                            <div class="row">
                                <div class="col-md-6" id="Div3" runat="server">

                                    <asp:Label ID="lblfrmDesig" runat="server">Form</asp:Label>
                                    <asp:DropDownList ID="ddlfrmDesig" runat="server" OnSelectedIndexChanged="ddlfrmDesig_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2 form-control-sm" TabIndex="6">
                                    </asp:DropDownList>

                                </div>

                                <div class="col-md-6" id="Div4" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lbltoDesig" runat="server">To</asp:Label>
                                        <asp:DropDownList ID="ddlToDesig" runat="server" CssClass="form-control form-control-sm select2" TabIndex="6">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-2" id="RbtnAttanTypeDiv" runat="server">
                            <asp:Label ID="Label2" runat="server">Attentdance Type</asp:Label>

                            <asp:RadioButtonList ID="RbtnAttanType" runat="server" AutoPostBack="True"
                                CssClass="custom-control custom-checkbox d-block p-0 mt-2"
                                Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="28" Selected="True">Machine</asp:ListItem>
                                <asp:ListItem Value="29">Manual</asp:ListItem>
                                <asp:ListItem Value="">Both</asp:ListItem>

                            </asp:RadioButtonList>

                        </div>
                                         <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="Label14" runat="server">Page Size</asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                                <asp:ListItem>600</asp:ListItem>
                                <asp:ListItem>1000</asp:ListItem>
                                <asp:ListItem>2000</asp:ListItem>
                                <asp:ListItem>3000</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-1">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm mt-4" OnClick="lnkbtnShow_Click">Show</asp:LinkButton>
                        </div>
                    </div>

                </div>
                <div class="card-body">
                    <div class="row mb-2">
                        <asp:Panel runat="server" ID="pnlAttnLog" Visible="false">
                            <div class="table-responsive" runat="server">
                                <asp:GridView ID="gvAttnLog" runat="server" PageSize="15"
                                    AutoGenerateColumns="False" ShowFooter="True" AllowPaging="True" OnPageIndexChanging="gvAttnLog_PageIndexChanging"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.L">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAttnSL" runat="server" Font-Bold="True" Height="14px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAttnLogName" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ID Card">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAttnLogIdCard" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAttnLogDept" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "depname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAttnLogDesig" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "desg")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAttnLogDesig" runat="server" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Log Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAttnLogDesig" runat="server" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "logs")).ToString("HH:mm tt") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" VerticalAlign="Middle" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlmonthlyatt" Visible="false">
                            <div class="table-responsive" id="DelaisAttinfo" runat="server">
                                <asp:GridView ID="gvMonthlyAtt" runat="server" PageSize="15"
                                    AutoGenerateColumns="False" ShowFooter="True" AllowPaging="True" OnPageIndexChanging="gvMonthlyAtt_PageIndexChanging"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCode" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno"))%>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                    </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Id #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgidcard" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />

                                            <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">

                                                     <HeaderTemplate>
                                                    <asp:Label ID="lblexle" runat="server" Font-Bold="True" Width="100px"
                                                        Text="Section">
                                                        <asp:HyperLink ID="hlbtntbCdataExelSP" runat="server"
                                                            CssClass="btn btn-success ml-2 btn-xs" ToolTip="Export Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                    </asp:Label>
                                                </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvName" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empnam")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">

                                      



                                            <ItemTemplate>
                                                <asp:Label ID="lgvdept" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdsig" runat="server"  Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empdsg")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                    </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Day">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvday" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "addday")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="01">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv01" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col1"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col1o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="02">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv02" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col2"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col2o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="03">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv03" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col3"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col3o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="04">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv04" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col4"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col4o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="05">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv05" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col5"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col5o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="06">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv06" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col6"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col6o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="07">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv07" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col7"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col7o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="08">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv08" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col8"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col8o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="09">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv09" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col9"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col9o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv10" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col10"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col10o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="11">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv11" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col11"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col11o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="12">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv12" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col12"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col12o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="13">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv13" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col13"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col13o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="14">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv14" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col14"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col14o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="15">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv15" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col15"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col15o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="16">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv16" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col16"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col16o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="17">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv17" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col17"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col17o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="18">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv18" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col18"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col18o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="19">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv19" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col19"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col19o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="20">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv20" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col20"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col20o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="21">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv21" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col21"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col21o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="22">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv22" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col22"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col22o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="23">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv23" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col23"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col23o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="24">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv24" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col24"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col24o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="25">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv25" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col25"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col25o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="26">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv26" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col26"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col26o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="27">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv27" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col27"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col27o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="28">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv28" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col28"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col12o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="29">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv29" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col29"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col29o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="30">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv30" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col30"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col30o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="31">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv31" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col31"))
                                            +"<br/>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "col31o")) %>'
                                                    Width="33px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>


                            <div class="table-responsive" id="SummaryAttinfo" runat="server">
                                <div class="col-3">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend ">
                                            <asp:Label ID="Label3" runat="server" CssClass="btn btn-secondary btn-sm">Search</asp:Label>
                                        </div>
                                        <asp:TextBox ID="inputtextbox" Style="height: 29px" runat="server" CssClass="form-control" placeholder="Search here..." onkeyup="Search_Gridview(this)"></asp:TextBox>

                                    </div>
                                </div>
                                <br />
                                <asp:GridView ID="gvMonthlyattSummary" runat="server" AutoGenerateColumns="False" ShowFooter="True" AllowPaging="true"
                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnPageIndexChanging="gvMonthlyattSummary_PageIndexChanging1">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgidcardsumm" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />

                                            <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                                         <HeaderTemplate>
                                                    <asp:Label ID="lblexle2" runat="server" Font-Bold="True" Width="100px"
                                                        Text="Section">
                                                        <asp:HyperLink ID="hlbtntbCdataExelSP2" runat="server"
                                                            CssClass="btn btn-success ml-2 btn-xs" ToolTip="Export Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                    </asp:Label>
                                                </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgvNameSumm" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empnam")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />

                                            <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Middle" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="01" HeaderStyle-Wrap="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv01summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col1s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col1s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col1s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="02">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv02summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col2s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col2s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col2s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="03">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv03summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col3s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col3s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col3s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="04">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv04summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col4s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col4s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col4s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="05">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv05summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col5s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col5s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col5s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="06">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv06summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col6s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col6s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col6s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="07">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv07summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col7s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col7s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col7s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="08">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv08summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col8s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col8s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col8s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="09">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv09summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col9s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col9s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col9s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv10summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col10s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col10s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col10s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="11">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv11summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col11s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col11s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col11s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="12">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv12summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col12s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col12s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col12s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="13">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv13summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col13s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col13s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col13s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="14">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv14summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col14s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col14s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col14s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="15">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv15summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col15s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col15s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col15s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="16">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv16summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col16s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col16s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col16s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="17">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv17summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col17s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col17s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col17s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="18">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv18summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col18s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col18s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col18s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="19">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv19summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col19s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col19s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col19s"))
                                         %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="20">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv20summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col20s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col20s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col20s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="21">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv21summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col21s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col21s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col21s"))
                                        %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="22">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv22summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col22s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col22s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col22s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="23">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv23summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col23s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col23s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col23s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="24">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv24summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col24s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col24s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col24s"))
                                          %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="25">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv25summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col25s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col25s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col25s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="26">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv26summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col26s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col26s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col26s"))
                                       %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="27">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv27summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col27s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col27s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col27s"))
                                           %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="28">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv28summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col28s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col28s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col28s"))
                                            %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="29">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv29summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col29s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col29s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col29s"))
                                          %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="30">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv30summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col30s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col30s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col30s"))
                                          %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="31">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv31summ" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col31s"))=="L"?"bg-yellow d-block":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "col31s"))=="LP"?"bg-danger d-block":""%>'
                                                    runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col31s"))
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Pesent">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPresent" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "present")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Late">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllate" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "late")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="LP">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllateabs" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "abslate")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Absent">
                                            <ItemTemplate>
                                                <asp:Label ID="lblabsent" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Holiday">
                                            <ItemTemplate>
                                                <asp:Label ID="lblholiday" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "holyday")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Leave">
                                            <ItemTemplate>
                                                <asp:Label ID="lblleave" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tleav")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotal" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tpayable")).ToString("#,##0; ")
                                             %>'
                                                    Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>

                                <fieldset class="scheduler-border fieldset_A" id="StatusReport" runat="server" visible="false">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class=" row col-md-11">
                                                <span style="font-size: 14px; color: blue" id="statusatt" runat="server">Present=P,  Absent =A,  Late=L,  Late Present=LP,  Casual Leave=CL, Sick Leave=SL, Earned Leave=EL, Without Pay Leave= WPL,  Weekend=W, Special Thursday=ST </span>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </asp:Panel>

                        <asp:Panel runat="server" ID="pnldailyatt" Visible="false">
                            <div class="table-responsive">
                                <asp:GridView ID="gvdailyatt" runat="server" PageSize="15"
                                    AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gvdailyatt_RowDataBound"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.L">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSl" runat="server" Font-Bold="True" Height="14px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempid" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcardno" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdsignation" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="In">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoffin" runat="server" Font-Size="11px" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("hh:mm tt") %>'
                                                    Width="45px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Out">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoffout" runat="server" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")).ToString("hh:mm tt") %>'
                                                    Width="45px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="In">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvactin" runat="server" Font-Size="11px" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'
                                                    Width="45px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Out">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvactout" runat="server" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("hh:mm tt") %>'
                                                    Width="45px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Late">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlate" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "late")) %>'
                                                    Width="33px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Early <br/> Leave">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvearlylv" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "eleave")) %>'
                                                    Width="33px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Leave <br/>/ Absent">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvabs" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "status")) %>'
                                                    Width="33px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>

                        <asp:Panel runat="server" ID="pnlempstatus" Visible="false">
                            <div class="table-responsive">
                                <asp:GridView ID="gvEmpStatus" runat="server" PageSize="15"
                                    AutoGenerateColumns="False" ShowFooter="True"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.L">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlst" runat="server" Font-Bold="True" Height="14px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Id #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempidst" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Id #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcardnost" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempnamest" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empnam")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdsignationst" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empdsg")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdatest" runat="server" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "wintime")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoffinst" runat="server" Font-Size="11px" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt") %>'
                                                    Width="45px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Out Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoffoutst" runat="server" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt") %>'
                                                    Width="45px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsts" runat="server" Font-Size="11px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "leav"))%>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Penalty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpenalty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedtimePenal1")) %>'
                                                    Width="50px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Official Hour">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoffh" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actTimehour")) %>'
                                                    Width="33px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>




                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>

                        <asp:Panel runat="server" ID="pnlemplateatt" Visible="false">
                            <div class="table-responsive">
                                <asp:GridView ID="gvemplateatt" runat="server" PageSize="15"
                                    AutoGenerateColumns="False" ShowFooter="True"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.L">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSllt" runat="server" Font-Bold="True" Height="14px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempidlt" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcardnolt" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempnamelt" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdsignationlt" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empdsg")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Late Day">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdatelt" runat="server" Text='<%#  Convert.ToInt32(DataBinder.Eval(Container.DataItem, "today"))%>'
                                                    Width="70px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>




                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>

                        <asp:Panel runat="server" ID="pnlempstatusLate" Visible="false">
                            <div class="table-responsive">
                                <asp:GridView ID="gvempstatusLate" runat="server" PageSize="15"
                                    AutoGenerateColumns="False" ShowFooter="True"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.L">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlstlt" runat="server" Font-Bold="True" Height="14px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempidstlt" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcardnostlt" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempnamestlt" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empnam")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdsignationstlt" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empdsg")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdatestlt" runat="server" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "wintime")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoffinstlt" runat="server" Font-Size="11px" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt") %>'
                                                    Width="45px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Out Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoffoutstlt" runat="server" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt") %>'
                                                    Width="45px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

