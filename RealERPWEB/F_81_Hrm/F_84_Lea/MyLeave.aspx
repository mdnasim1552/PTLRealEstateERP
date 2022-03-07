﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MyLeave.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.ApplyLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        table tr th {
            text-align: center;
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


                <div class="row">
                    <div class="col-12 col-lg-12 col-xl-3">
                        <section class="card card-fluid" style="min-height: 650px">
                            <!-- .card-body -->
                            <header class="card-header">Leave Apply</header>
                            <div class="card-body">

                                <div class="form-group">
                                    <label for="ddlLvType">Apply Date  <span id="sspnlv" class="text-danger" runat="server" visible="false">
                                            <asp:CheckBox ID="chkBoxSkippWH" AutoPostBack="true" ToolTip="If you want to skip weekend/holiday/special day, please click the checkbox and click individual date click" OnCheckedChanged="chkBoxSkippWH_CheckedChanged" Text=" Skip W,H, SP Day " runat="server" Checked="false" />
                                        </span></label>
                                    <asp:TextBox ID="txtaplydate" runat="server" AutoPostBack="true" class="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtaplydate"></cc1:CalendarExtender>
                                </div>

                                <div class="form-group">
                                    <label for="ddlLvType">
                                        Leave Type
                                    </label>
                                    <asp:DropDownList ID="ddlLvType" class="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="row">
                                    <!-- grid column -->
                                    <div class="col-md-6 pl-0">
                                        <!-- .form-group -->
                                        <div class="form-group">
                                            <label for="sel1">From Date </label>
                                            <asp:TextBox ID="txtgvenjoydt1" runat="server" OnTextChanged="txtgvenjoydt1_TextChanged1" AutoPostBack="true" class="form-control"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtgvenjoydt1_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt1"></cc1:CalendarExtender>
                                        </div>
                                        <!-- /.form-group -->
                                    </div>
                                    <!-- /grid column -->
                                    <!-- grid column -->
                                    <div class="col-md-6 pr-0" id="divBTWDay" runat="server">
                                        <div class="form-group">
                                            <label for="sel1">To Date </label>

                                            <asp:TextBox ID="txtgvenjoydt2" runat="server" OnTextChanged="txtgvenjoydt2_TextChanged" AutoPostBack="true" class="form-control"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtgvenjoydt2_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt2"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-6 pr-0" id="diSkippDay" runat="server" visible="false">
                                        <div class="form-group">
                                            <label for="Duration">Is Half Day</label>
                                            <asp:CheckBox ID="CheckBox1" class="form-control" ToolTip="" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" runat="server" Text=" Half Day Leave" />
                                        </div>
                                        

                                    </div>
                                    <!-- /grid column -->
                                </div>

                                <div class="row" id="diSkippDayDetails" runat="server" visible="false">
                                        <div class="form-group">

                                    <asp:GridView ID="gvInterstLev" runat="server" AutoGenerateColumns="False"
                                            CssClass="table-striped table-hover table-bordered" BorderWidth="0"
                                            ShowFooter="false" ShowHeader="false" OnRowDataBound="gvInterstLev_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Apply Date">
                                                    <ItemTemplate>
                                                        

                                                        <asp:Label ID="lgvapplydate" runat="server"  Width="150px" 
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "leavday")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                      
                                                        
                                                    </ItemTemplate>
                                                    
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="">
                                                    <ItemTemplate> 
                                                          <asp:LinkButton ID="LinkButton1"    CssClass="badge bg-purple text-white" ForeColor="blue" runat="server">
                                                            <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isHalfday"))=="True"?"Is Half Day":"" %>
                                                          </asp:LinkButton>
                                                    </ItemTemplate>
                                                    
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
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





                                            </Columns>

                                        </asp:GridView>
                                </div>
                                </div>

                                <div class="row" id="divDurStatus" runat="server">
                                    <div class="col-md-6 pl-0">
                                        <div class="form-group">
                                            <label for="Duration">Duration</label>
                                            <input type="text" class="form-control disabled" runat="server" disabled id="Duration" readonly="readonly" aria-describedby="tf1Help" placeholder="">
                                        </div>
                                    </div>
                                    <div class="col-md-6 pl-0">
                                        <div class="form-group">
                                            <label for="Duration">Half Day</label>
                                            <asp:CheckBox ID="chkHalfDay" class="form-control" OnCheckedChanged="chkHalfDay_CheckedChanged" AutoPostBack="true" runat="server" Text=" Half Day Leave" />
                                        </div>
                                    </div>



                                </div>

                                <div class="form-group">
                                    <label for="Reason">Reason/Remarks</label>
                                    <asp:TextBox ID="txtLeavLreasons" runat="server" placeholder="Reason" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="tf1">While on Leave, Duties will Performed by</label>
                                    <asp:TextBox ID="txtdutiesnameandDesig" runat="server" placeholder="While on Leave, Duties will Performed by" TextMode="MultiLine" class="form-control"></asp:TextBox>

                                </div>
                                <div class="form-group d-none">
                                    <label for="txtaddofenjoytime">Address of enjoing time</label>
                                    <asp:TextBox ID="txtaddofenjoytime" runat="server" placeholder="Address of enjoing time" TextMode="MultiLine" class="form-control"></asp:TextBox>

                                </div>
                                <div class="form-group d-none">
                                    <label for="txtLeavRemarks">Remarks</label>
                                    <asp:TextBox ID="txtLeavRemarks" runat="server" placeholder="Remarks" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                </div>

                                <div class="form-group text-right">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" CssClass="btn btn-sm btn-primary" />
                                </div>

                            </div>
                        </section>
                    </div>
                    <div class="col-12 col-lg-12 col-xl-9">
                        <section class="card card-fluid mb-0" style="min-height: 650px; flex-grow: 1; overflow: auto;">
                            <!-- .card-body -->
                            <header class="card-header">Applied Leave</header>

                            <div class="card-body card card-fluid mb-0">

                                <div class="row" style="height: 180px; flex-grow: 1; overflow: auto;">
                                    <asp:GridView ID="gvleaveInfo" runat="server" AutoGenerateColumns="False"
                                        CssClass="table-striped table-hover table-bordered"
                                        ShowFooter="True" OnRowDataBound="gvleaveInfo_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="trnleaveid" Visible="false">
                                                <ItemTemplate>
                                                       <asp:Label ID="lbllevid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'
                                                        Width="120px"></asp:Label>

                                                    <asp:Label ID="lgvltrnleaveid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete"
                                                        Visible='<%# Eval("isapproved").ToString() == "True" ? true : false %>'
                                                        runat="server" Font-Bold="True" ToolTip="Delete" OnClick="lnkDelete_Click">
                                                         <i class=" fa fa-trash"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>







                                            <asp:TemplateField HeaderText="Desription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvledescription" runat="server"
                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "grpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                        Width="250px">
                                                                
                                                                
                                                                
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Apply Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvapplydate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aplydat")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="From Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvlstartdate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstrtdat")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="End Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvlenddate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lenddat"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Leave Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvleavedays" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "enjoyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvreason" runat="server" Width="355px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreason")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvremarks" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lrmarks")) %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>

                                </div>
                                <div class="row mt">
                                    <header class="card-header">Leave Status</header>


                                    <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                        CssClass="table-striped table-hover table-bordered">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" />
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
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entitlement" ItemStyle-BackColor="#cccccc">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave <br> This Year" ItemStyle-BackColor="#999999">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="middle" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Present <br> Bal." ItemStyle-BackColor="#cccccc">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requested">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "applyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Requested <br> Std. Date">
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
                                            <asp:TemplateField HeaderText="Closing <br> Bal." ItemStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Leave <br> Std. Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvenjoydt10" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Leave <br> End Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvleavedt20" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                                        Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Leave <br> Day's">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvenjoyday0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
            </div>


 

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
