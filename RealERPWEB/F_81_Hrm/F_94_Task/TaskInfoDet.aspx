<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="TaskInfoDet.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_94_Task.TaskInfoDet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .mt20 {
            margin-top: 20px;
        }

        div#ContentPlaceHolder1_ddldeptcode_chzn {
            width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlTdeptcode_chzn {
            width: 100% !important;
        }
        div#ContentPlaceHolder1_ddltask_chzn{
                 width: 100% !important;
        }
        div#ContentPlaceHolder1_ddldept1_chzn{
                 width: 100% !important;

        }

        div#ContentPlaceHolder1_ddltloc_chzn{
              width: 100% !important;
        }
        div#ContentPlaceHolder1_ddlfloc_chzn{
             width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlEmp_chzn{
 width: 100% !important;
        }
        .chzn-drop {
            width: 100% !important;
        }
                                .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
                .card-body{
                    min-height:400px!important;
                }
                                  .pd4{
                    padding:4px!important;
                }
    </style>


    <script type="text/javascript">
        //$(document).ready(function () {
        //    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        //});
        function openModal() {
            //    $('#myModal').modal('show');
            $('#SearchModal').modal('toggle');
        }

        function CloseModal() {

            $('#SearchModal').modal('hide');
        }







        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>




    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
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



    <div class="card mt-5">
        <div class="card-body" style="min-height: 600px;">
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">

                    <div class="row">
                         <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <label class="control-label">Employee:</label>
                                <asp:DropDownList ID="ddlEmp" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>


                            </div>
                        </div>
                <div class="col-lg-1 col-md-1 col-sm-6">
                            <div class="form-group">
                                <label class="control-label">From:</label>
                                <asp:TextBox ID="txtfmdt1" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfmdt1"></cc1:CalendarExtender>

                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <div class="form-group">

                                <label class="control-label">To:</label>
                                <asp:TextBox ID="txttodt1" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodt1"></cc1:CalendarExtender>


                            </div>
                        </div>
                                <div class="col-lg-1 col-md-1 col-sm-6">
                            <label class="control-label">Page:</label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                                <asp:ListItem>600</asp:ListItem>
                                <asp:ListItem>900</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                                 <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:LinkButton ID="lnkSelect" runat="server" CssClass="btn btn-primary btn-sm mt-4" OnClick="lnkSelect_Click" TabIndex="11" Text="Show"></asp:LinkButton>

                        </div>
                                 <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:LinkButton ID="lnkNewTask" runat="server" CssClass="btn btn-primary btn-sm mt-4" OnClick="lnkNewTask_Click" TabIndex="11">Add Today's Activities</asp:LinkButton>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <asp:GridView ID="gvShowData" runat="server" AllowPaging="True" OnRowDataBound="gvShowData_RowDataBound"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="600px" OnRowCommand="gvShowData_RowCommand"
                                    PageSize="15">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                                        Visible="False" />
                                    <%-- <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="10px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Font-Bold="True" Height="16px" ToolTip="Delete" Style="text-align: right" OnClientClick="javascript:return  FunConfirm()" OnClick="lnkDelete_Click"><span class=" fa   fa-recycle"></span></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbfdate" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "fdate")).ToString("dd-MMM-yyyy")%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemployeee" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Text='<%#(DataBinder.Eval(Container.DataItem, "empdesc")).ToString()%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="From Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblftime" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Text='<%#(DataBinder.Eval(Container.DataItem, "ftime")).ToString()%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblttime" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Text='<%#(DataBinder.Eval(Container.DataItem, "ttime")).ToString()%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Duration">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldur" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Activity Description">
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemTemplate>

                                                <asp:Label ID="lbltaskdesc" runat="server" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="10px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Details">


                                            <ItemTemplate>

                                                <asp:Label ID="lbltdesc" runat="server" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="10px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltaskcode" runat="server" Font-Size="10px" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskcode")) %>'
                                                    Width="40px"></asp:Label>
                                                <asp:Label ID="lblrowidA" runat="server" Font-Size="10px" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")) %>'
                                                    Width="40px"></asp:Label>
                                                <asp:Label ID="lblfloctncode" runat="server" Font-Size="10px" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "floctn")) %>'
                                                    Width="40px"></asp:Label>
                                                <asp:Label ID="lbltloctncode" runat="server" Font-Size="10px" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tloctn")) %>'
                                                    Width="40px"></asp:Label>
                                                <asp:Label ID="lblempid" runat="server" Font-Size="10px" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empcode")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="From Location ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfloctn" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "floctndesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To Location">
                                            <ItemTemplate>

                                                <asp:Label ID="lbltloctn" runat="server" Font-Size="10px" Style="font-size: 10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tloctndesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrmk" runat="server" Font-Size="10px"
                                                    Text='<%# (DataBinder.Eval(Container.DataItem, "rmk")).ToString() %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnedit" runat="server" OnClick="btnedit_Click" ToolTip="Edit"><span class="fa fa-edit"></span></asp:LinkButton>

                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="10px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <AlternatingRowStyle BackColor="" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>





                </asp:View>

                <asp:View ID="View2" runat="server">
                    <hr />


                    <div class="row mt-2">
                        <div class="col-lg-2">
                            <label class="control-label">Date:</label>
                        </div>

                        <div class="col-lg-3">
                       <asp:TextBox ID="txtdateentry" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdateentry"></cc1:CalendarExtender>

                        </div>


                        <div class="col-lg-2">
                            <label class="control-label">Department:</label>
                        </div>

                        <div class="col-lg-3">
       <asp:DropDownList ID="ddldept1" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddldeptcode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row mt-3">

                        <div class="col-lg-2">
                            <label class="control-label">Task:</label>
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList ID="ddltask" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>

                        <div class="col-lg-2">
                            <label class="control-label">Description:</label>
                        </div>
                        <div class="col-lg-3">
                            <asp:TextBox ID="txttaskdesc" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>
                        <asp:Label runat="server" ID="lblrowid" Visible="false">0</asp:Label>
                        <asp:Label runat="server" ID="lblempid" Visible="false"></asp:Label>

                    </div>

                    <div class="row mt-2">
                        <div class="col-lg-2 ">
                            <label class="control-label">Location From:</label>
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList ID="ddlfloc" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 ">
                            <label class="control-label">Location To:</label>
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList ID="ddltloc" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-1">
                            <asp:LinkButton ID="lnkLocCodeBook" runat="server" CssClass="btn btn-primary" TabIndex="11" OnClick="lnkLocCodeBook_Click" ToolTip="Add Location"><i class="fas fa-plus"></i></asp:LinkButton>
                        </div>

                    </div>

                    <div class="row mt-2">
                        <div class="col-lg-2 ">
                            <label class="control-label">From:</label>
                        </div>
                        <div class="col-lg-3">
                            <%--<asp:DropDownList ID="ddlhour" runat="server" CssClass="inputTxt ddlPage" Style="width: 50px; line-height: 22px;">
                                <asp:ListItem Value="1">01</asp:ListItem>
                                <asp:ListItem Value="2">02</asp:ListItem>
                                <asp:ListItem Value="3">03</asp:ListItem>
                                <asp:ListItem Value="4">04</asp:ListItem>
                                <asp:ListItem Value="5">05</asp:ListItem>
                                <asp:ListItem Value="6">06</asp:ListItem>
                                <asp:ListItem Value="7">07</asp:ListItem>
                                <asp:ListItem Value="8">08</asp:ListItem>
                                <asp:ListItem Value="9" Selected="True">09</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="11">11</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>

                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlMmin" runat="server" CssClass="ddlPage" Style="width: 50px; line-height: 22px;">
                                <asp:ListItem Value="0">00</asp:ListItem>
                                <asp:ListItem Value="5">05</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="25">25</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="35">35</asp:ListItem>
                                <asp:ListItem Value="40">40</asp:ListItem>
                                <asp:ListItem Value="45">45</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="55">55</asp:ListItem>

                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlslb" runat="server" CssClass="ddlPage" Style="width: 50px; line-height: 22px;">
                                <asp:ListItem Value="AM">AM</asp:ListItem>
                                <asp:ListItem Value="PM">PM</asp:ListItem>
                            </asp:DropDownList>--%>

                            <asp:TextBox runat="server" ID="fdate" CssClass="form-control form-control-sm" TextMode="DateTimeLocal"></asp:TextBox>

                        </div>
                        <div class="col-lg-2 ">
                            <label class="control-label">To:</label>
                        </div>
                        <div class="col-lg-3">
                            <%-- <asp:DropDownList ID="ddlhourT" runat="server" CssClass="inputTxt ddlPage" Style="width: 50px; line-height: 22px;">
                                <asp:ListItem Value="1">01</asp:ListItem>
                                <asp:ListItem Value="2">02</asp:ListItem>
                                <asp:ListItem Value="3">03</asp:ListItem>
                                <asp:ListItem Value="4">04</asp:ListItem>
                                <asp:ListItem Value="5">05</asp:ListItem>
                                <asp:ListItem Value="6">06</asp:ListItem>
                                <asp:ListItem Value="7">07</asp:ListItem>
                                <asp:ListItem Value="8">08</asp:ListItem>
                                <asp:ListItem Value="9" Selected="True">09</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="11">11</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>

                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlMminT" runat="server" CssClass="ddlPage" Style="width: 50px; line-height: 22px;">
                                <asp:ListItem Value="0">00</asp:ListItem>
                                <asp:ListItem Value="5">05</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="25">25</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="35">35</asp:ListItem>
                                <asp:ListItem Value="40">40</asp:ListItem>
                                <asp:ListItem Value="45">45</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="55">55</asp:ListItem>

                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlslbT" runat="server" CssClass="ddlPage" Style="width: 50px; line-height: 22px;">
                                <asp:ListItem Value="AM">AM</asp:ListItem>
                                <asp:ListItem Value="PM">PM</asp:ListItem>


                            </asp:DropDownList>--%>
                            <asp:TextBox runat="server" ID="tdate" CssClass="form-control form-control-sm" TextMode="DateTimeLocal"></asp:TextBox>
                        </div>
                        <div class="col-lg-1">
                            <%--<asp:Label runat="server" ID="lblhr" class="control-label">3 hours</asp:Label>--%>
                        </div>

                    </div>
                    <div class="row mt-2">
                        <div class="col-lg-2 ">
                            <label class="control-label">Remarks:</label>
                        </div>
                        <div class="col-lg-3">
                            <asp:TextBox ID="txtrem" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>

                        </div>


                        <div class="col-lg-2">
                          
                        </div>

                        <div class="col-lg-2">
                              <asp:LinkButton ID="btnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnOk_Click" TabIndex="11">Add</asp:LinkButton>

                            <asp:LinkButton ID="lnkBack" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkBack_Click" TabIndex="11">Back</asp:LinkButton>

                            </div>
                    </div>








                </asp:View>
                <asp:View runat="server">

                    <div class="row mt-3">
                        <div class="col-7">
                            <asp:GridView ID="grvacc" runat="server" CssClass=" table-condensed table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False"
                                CellPadding="4" Font-Bold="False" Font-Size="10px"
                                OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                                OnRowUpdating="grvacc_RowUpdating" Width="572px" ShowFooter="True">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                                    Visible="False" />
                                <FooterStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="10px" />
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                        SelectText="" ShowEditButton="True">
                                        <HeaderStyle Font-Size="16px" />
                                        <ItemStyle Font-Bold="True" Font-Size="10px" ForeColor="#0000C0" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrcode" runat="server" Font-Size="10px" Height="16px"
                                                MaxLength="3"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 10px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod3")) %>'
                                                Width="40px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod3" runat="server" Font-Size="10px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod3")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="16px" />
                                        <ItemStyle Font-Size="10px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="10px" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 10px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                                Width="250px"></asp:TextBox>
                                        </EditItemTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lbldesc" runat="server" Font-Size="10px"
                                                Style="font-size: 10px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="16px" HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod")) %>'
                                                Visible="False"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvttpe" runat="server" BackColor="White" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgval")) %>'
                                                Width="30px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtype" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgval")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </div>
                        <div class="col-5">
                            <asp:LinkButton ID="lnkbacktoentry" runat="server" CssClass="btn btn-primary" TabIndex="11" OnClick="lnkbacktoentry_Click">Back To Entry List</asp:LinkButton>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>


        </div>

    </div>


    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

