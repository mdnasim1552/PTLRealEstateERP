<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpLvApproval.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.EmpLvApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
        };


    </script>

    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>




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

            <div class="card-fluid container-data  mt-2">
                <div class="row" id="warning" runat="server" visible="false">
                    <div class="col-12 col-lg-12 col-xl-12">
                        <div class="section-block">
                            <div class="alert alert-danger has-icon" role="alert">
                                <div class="alert-icon">
                                    <span class="fa fa-bullhorn"></span>
                                </div>
                                The Department: <b><span id="dptNameset" runat="server"></span></b>Approval user  did not set.
                                <br />
                                Please Contact you HR Department 
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="divApproval" runat="server" visible="false">
                    <div class="col-sm-12 col-md-12 col-lg-12 col-xl-12">
                        <div class="section-block">
                            <div class="alert alert-danger has-icon" role="alert">
                                <div class="alert-icon">
                                    <span class="fa fa-bullhorn"></span>
                                </div>
                                The leave already has been approved!.
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" id="levapp" runat="server">
                    <div class="col-12 col-lg-12 col-xl-3">
                        <section class="card card-fluid" style="height: 650px">
                            <header class="card-header">LEAVE APPROVAL</header>

                            <div class="card-body">
                                <div class="form-group">

                                    <label for="Date" class="col-md-12">
                                        Date 
                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary btn-sm float-right okBtn" OnClick="lnkOk_Click">Ok</asp:LinkButton>
                                    </label>

                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control "></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                </div>
                                <div class="form-group">
                                    <label for="ddlLvType">Center Name</label>
                                    <asp:DropDownList ID="ddlCenter" runat="server" CssClass="form-control chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCenter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <asp:Panel ID="Panel2" runat="server" Visible="false">
                                    <div class="form-group">
                                        <asp:ListBox ID="lstOrderNo" runat="server" AutoPostBack="True" CssClass="form-control"
                                            BackColor="#DFF0D8" Font-Bold="True" Font-Size="12px" Height="50px"
                                            OnSelectedIndexChanged="lstOrderNo_SelectedIndexChanged"
                                            SelectionMode="Multiple" TabIndex="12"></asp:ListBox>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PnlNarration" runat="server" Visible="false">
                                    <div class="form-group">
                                        <label for="Narration">Narration</label>
                                        <asp:TextBox ID="lblvalNarration" runat="server" class="form-control" Rows="1" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="ApprovedBy">Approved By</label>
                                        <asp:TextBox ID="lblRemarks" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="ApprovedBy">Remaks</label>
                                        <asp:TextBox ID="txtremarks" runat="server" class="form-control" Rows="1" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-md-6 pl-0">
                                        <div class="form-group">
                                            <label id="chkbod" runat="server" class="switch">
                                                <asp:CheckBox ID="Chboxforward" runat="server" />
                                                <span class="btn btn-xs slider round"></span>
                                            </label>
                                            <asp:Label ID="lblforward" runat="server" Text="Forward" CssClass="btn btn-xs"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-6 pl-0">
                                        <div class="form-group">
                                            <asp:LinkButton ID="ApprovedBtn" runat="server" CssClass="btn btn-primary btn-sm ApprovedBtn d-none" BorderStyle="None">Approved</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                    <div class="col-12 col-lg-12 col-xl-9">
                        <section class="card card-fluid" style="height: 650px">
                            <header class="card-header">Leave Information (<span id="spEmpInfo" class="text-danger" runat="server"></span>)</header>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-9">
                                        <asp:GridView ID="gvLvReq" runat="server" AutoGenerateColumns="False"
                                            CssClass="table-striped table-hover table-bordered" OnRowDataBound="gvLvReq_RowDataBound"
                                            ShowFooter="True">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkIntsLvDelete" Width="50px" ForeColor="Red"
                                                            runat="server" ToolTip="Delete" OnClick="lnkIntsLvDelete_Click">
                                                         <i class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbllevid" runat="server" ForeColor="Red"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'
                                                            Width="120px"></asp:Label>
                                                        <asp:Label ID="Labempuserid" runat="server" ForeColor="Red"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empuserid")) %>'
                                                            Width="120px"></asp:Label>


                                                        <asp:Label ID="lgvltrnleaveid" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                            Width="120px"></asp:Label>

                                                        <asp:Label ID="lblgvempid" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="gcode" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvgcod" runat="server" Visible="False"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                            Width="49px"></asp:Label>
                                                        <asp:TextBox ID="lblgvhrdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                            Width="49px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Emp Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvempname" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                            Width="160px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID Card" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgidcard" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                            Width="80px"></asp:Label>

                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgdeptanme" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                            Width="120px"></asp:Label>

                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgdesig" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                            Width="120px"></asp:Label>

                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lglvtype" runat="server" Visible="false"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                            Width="150px"></asp:Label>

                                                        <asp:DropDownList ID="ddlLvtype" runat="server" AutoPostBack="true" CssClass="form-control p-1"></asp:DropDownList>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn  btn-warning btn-sm" OnClick="lbtnTotal_Click">Recalculate days</asp:LinkButton>

                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Apply Date">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>


                                                        <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-danger btn-sm ApprovedBtn" OnClick="lbtnDelete_Click" BorderStyle="None">Cancel</asp:LinkButton>

                                                    </FooterTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Applied For">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvlapplied" runat="server" BorderStyle="None"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px" BackColor="Transparent" Font-Size="12px"
                                                            Style="text-align: right"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Start Date">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvlstdate" runat="server" BorderStyle="None" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px" BackColor="Transparent" Font-Size="12px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtgvlstdate_CalendarExtender" runat="server" Enabled="True"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtgvlstdate"></cc1:CalendarExtender>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="LinkButton1_test" runat="server" CssClass="btn btn-info  btn-sm ApprovedBtn" OnClick="LinkButton1_test_Click" BorderStyle="None">Approved</asp:LinkButton>


                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="End Date">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddat")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Is Half Days">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ishalfday" runat="server" ToolTip="Only is Half day then click"
                                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ishalfday"))=="True" %>' />
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>






                                            </Columns>

                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                        </asp:GridView>
                                    </div>
                                         <div class="col-3">
                                                  <asp:panel runat="server" ID="pnlFinly" Visible="false">
                      
                                        <p class="m-0 badge text-white bg-primary">Current Leave Status</p>
                                        <table class="table-striped table-hover table-bordered text-center">
                                            <tr class="bg-primary text-white">
                                                <td>Leave</td>
                                                <td>Allowed</td>
                                                <td>Enjoy</td>
                                                <td>Balance</td>
                                            </tr>
                                            <tr>
                                                <td>EARNED LEAVE</td>
                                                <td> <asp:Label ID="elvallow" runat="server">0</asp:Label></td>
                                                <td><asp:Label ID="elvenjoy" runat="server"></asp:Label> </td>
                                                <td> <asp:Label ID="elvbalanc" runat="server">0</asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>CASUAL LEAVE</td>
                                                <td> <asp:Label ID="clvallow" runat="server">0</asp:Label></td>
                                                <td><asp:Label ID="clvenjoy" runat="server"></asp:Label> </td>
                                                <td> <asp:Label ID="clvbalanc" runat="server">0</asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>SICK LEAVE</td>
                                               <td> <asp:Label ID="slvallow" runat="server">0</asp:Label></td>
                                                <td><asp:Label ID="slvenjoy" runat="server"></asp:Label> </td>
                                                <td> <asp:Label ID="slvbalanc" runat="server">0</asp:Label></td>
                                            </tr>
                                        </table>

                              
                                    </asp:panel>
                                    <asp:panel runat="server" ID="pnlCommon">
                                      
                                        <p class="m-0 badge text-white bg-primary">Current Leave Status</p>
                                        <table class="table-striped table-hover table-bordered text-center">
                                            <tr>
                                                <td>Type</td>
                                                <td>Achieved</td>
                                                <td>Enjoy</td>
                                            </tr>
                                            <tr>
                                                <td>EARNED LEAVE</td>
                                                <td>
                                                    <asp:Label ID="lblelv" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblelvenjoy" runat="server">0</asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>CASUAL LEAVE</td>
                                                <td>
                                                    <asp:Label ID="lblclv" runat="server">0</asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblclenj" runat="server">0</asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>SICK LEAVE</td>
                                                <td>
                                                    <asp:Label ID="lblslv" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblslenj" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>

                                
                                    </asp:panel>
                                         </div>
                               
                                    
                           
                                </div>
                                <div class="row">
                                    <div class="card-body">

                                        <asp:Label ID="lblDutesInfo" CssClass="d-block text-info" Font-Bold="true" runat="server"></asp:Label>

                                    </div>
                                </div>
                            </div>
                            <header class="card-header">Leave Status</header>
                            <div class="card-body">
                                <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                                    CssClass="table-striped table-hover table-bordered text-center">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desription">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDescription0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "entitle")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entitlement">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave This Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Present Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0.00;(#,##0.00);") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requested">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "applyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requested  Std. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlrequeststdat" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lrstrtdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lrstrtdat")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Approved">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0.00;(#,##0.00);") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Std. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvenjoydt10" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleavedt20" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                                    Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Day's">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvenjoyday0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <EditRowStyle />
                                    <AlternatingRowStyle />

                                </asp:GridView>
                            </div>

                        </section>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
