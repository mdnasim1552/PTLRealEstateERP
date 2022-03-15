<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="UploadLeavExcel.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.UploadLeavExcel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                    <div class="col-12 col-lg-12 col-xl-3">
                        <section class="card card-fluid" style="height: 650px">

                            <div class="card-body">

                                <div class="form-group">
                                    <label for="ddlLvType">Apply Date</label>
                                    <asp:TextBox ID="txtaplydate" runat="server" AutoPostBack="true" class="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtaplydate" TodaysDateFormat=""></cc1:CalendarExtender>
                                </div>
                                <div class="form-group">
                                    <label for="Company">Company</label>
                                    <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control pull-left" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                    </asp:DropDownList>

                                </div>

                                <div class="form-group">
                                    <label for="Company">Department</label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control chzn-select" TabIndex="7" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="Company">Section</label>
                                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control chzn-select" TabIndex="6" >
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group text-right">


                                    <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm okBtn" OnClick="lnkbtnShow_Click" Text="Show"></asp:LinkButton>

                                </div>
                               

                                    <asp:Panel ID="pnlDedEarnExcel" CssClass="form-group" runat="server" Visible="false">
                                <div class="form-group">

                                         <asp:FileUpload ID="fileuploadExcel" runat="server" class="form-control"   onchange="submitform();" />
                                           
                                    </div>
                                <div class="form-group text-right">
                                     <asp:LinkButton ID="lbtnDedorOtherEernExcelAdjust" runat="server" CssClass="btn btn-sm btn-info okBtn pull-left" ForeColor="White" ToolTip="Adjust Deduction" OnClick="lbtnDedorOtherEernExcelAdjust_Click">
                                            <i class="fas fa-file-excel"></i>&nbsp;&nbsp;Adjust</asp:LinkButton>
                                    </div>

                                    </asp:Panel>
                                 
                            </div>

                        </section>
                    </div>
                    <div class="col-12 col-lg-12 col-xl-9">
                        <section class="card card-fluid" style="height: 650px">
                            <header class="card-header">Leave Information From Excel</header>

                        <div class="card-body">
                            <asp:GridView ID="gvLeavExcel" runat="server"  CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvLeavExcel_PageIndexChanging"
                                ShowFooter="True" OnRowDeleting="gvLeavExcel_RowDeleting" OnRowDataBound="gvLeavExcel_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:CommandField ShowDeleteButton="True" DeleteText="" />
                                    <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvEmpId" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Section">
                                        <HeaderTemplate>
                                            <asp:HyperLink ID="hlbtntbCdataExeldeduct" runat="server" BackColor="#000066"
                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSectiondectuc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                Width="160px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Card #">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCardno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Employee Name & Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvEmpName" runat="server"
                                                Text='<%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField> 

                                     <asp:TemplateField HeaderText="start date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtstardate" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="65px"></asp:TextBox>
                                        </ItemTemplate>
                                        
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtenddate" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddate")).ToString("dd-MMM-yyyy") %>'
                                                Width="65px"></asp:TextBox>
                                        </ItemTemplate>
                                        
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField> 

                                    <asp:TemplateField HeaderText="EL">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtel" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "el")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="65px"></asp:TextBox>
                                        </ItemTemplate>
                                        
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="CL">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtcl" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cl")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="65px"></asp:TextBox>
                                        </ItemTemplate>
                                        
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                      <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtsl" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sl")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="65px"></asp:TextBox>
                                        </ItemTemplate>
                                        
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                       <asp:TemplateField HeaderText="Star">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtstar" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "star")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="65px"></asp:TextBox>
                                        </ItemTemplate>
                                        
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    
                                       <asp:TemplateField HeaderText="Total days">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtleaveday" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leaveday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="65px"></asp:TextBox>
                                        </ItemTemplate>
                                        
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Is hal fday">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ishalfday" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ishalfday"))=="True" %>' />                                            
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
                            </section>
                    </div>
                </div>




            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
