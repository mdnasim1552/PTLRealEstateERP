<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ApplyLeave.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.ApplyLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    <div class="col-12 col-lg-12 col-xl-4">
                        <section class="card card-fluid">
                            <!-- .card-body -->
                            <header class="card-header"> Leave Apply</header>
                            <div class="card-body">

                                <div class="form-group">
                                    <label for="ddlLvType">Apply Date</label>
                                    <asp:TextBox ID="txtaplydate" runat="server" AutoPostBack="true" class="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtaplydate" TodaysDateFormat=""></cc1:CalendarExtender>
                                </div>

                                <div class="form-group">
                                    <label for="ddlLvType">Leave Type</label>
                                    <asp:DropDownList ID="ddlLvType" class="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="row">
                                    <!-- grid column -->
                                    <div class="col-md-6 pl-0">
                                        <!-- .form-group -->
                                        <div class="form-group">
                                            <label for="sel1">From Date</label>
                                            <asp:TextBox ID="txtgvenjoydt1" runat="server" AutoPostBack="true" class="form-control"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtgvenjoydt1_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt1"></cc1:CalendarExtender>
                                        </div>
                                        <!-- /.form-group -->
                                    </div>
                                    <!-- /grid column -->
                                    <!-- grid column -->
                                    <div class="col-md-6 pr-0">
                                        <div class="form-group">
                                            <label for="sel2">To Date</label>
                                            <asp:TextBox ID="txtgvenjoydt2" runat="server" OnTextChanged="txtgvenjoydt2_TextChanged" AutoPostBack="true" class="form-control"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtgvenjoydt2_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt2"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <!-- /grid column -->
                                </div>

                                <div class="row">
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
                    <div class="col-12 col-lg-12 col-xl-8">
                        <section class="card card-fluid">
                            <!-- .card-body -->
                            <header class="card-header">Appliyed Leave</header>

                            <div class="card-body">
                                <legend>Appliyed Leave</legend>
                                <asp:GridView ID="gvleaveInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="420px" OnRowDataBound="gvleaveInfo_RowDataBound"
                                    OnRowDeleting="gvleaveInfo_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="trnleaveid" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvltrnleaveid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="True" />

                                        <asp:TemplateField HeaderText="Desription">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvledescription" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "grpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="120px">
                                                                
                                                                
                                                                
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
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "enjoyday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreason" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreason")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvremarks" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lrmarks")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>




                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                <legend>Leave Status</legend>

                                <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="925px">
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
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entitlement">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave This Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Present Bal.">
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
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
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
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </section>
                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
