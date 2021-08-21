<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SalesRegister.aspx.cs" Inherits="RealERPWEB.F_22_Sal.SalesRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

        }
    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                

                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="View1" runat="server">
                            <asp:Panel ID="Panel2" runat="server" Style="margin:5px;">
                             <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                            

                                <div class="form-group">
                                    <div class="col-md-10  pading5px  asitCol10">
                                        <asp:Label ID="Label15" runat="server" CssClass="lblName lblTxt" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox" TabIndex="0"></asp:TextBox>

                                        <asp:LinkButton ID="ibtnFindProject"  runat="server"  OnClick="ibtnFindProject_Click"  CssClass="btn btn-primary srearchBtn" TabIndex="1"> <span class="glyphicon glyphicon-search asitGlyp"></span> </asp:LinkButton>

                                        <asp:DropDownList ID="ddlProjectName" CssClass="ddlPage" runat="server" Width="350px"></asp:DropDownList>

                                        <asp:Label ID="lblProjectdesc" runat="server" Visible="False" CssClass="inputtextbox" Width="350px"></asp:Label>
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="3">Ok</asp:LinkButton>

                                    </div>
                                </div>

                          
                                 </div>
                                  </asp:Panel>
                           <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                            <asp:Panel ID="PanelDetails" runat="server" Visible="False" Style="margin:8px;">
                                <div class="form-group">
                                    <div class="col-md-12  pading5px  asitCol10">
                                        <asp:Label ID="Label16" runat="server" CssClass="lblName lblTxt" Text="Unit Name:"></asp:Label>
                                        <asp:TextBox ID="txtUnitName" Width="200" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                        <asp:Label ID="Label17" runat="server" CssClass="lblName lblTxt" Text="Sale Date:"></asp:Label>
                                        <asp:TextBox ID="txtSaleDate" runat="server" CssClass="inputtextbox" AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtSaleDate_CalendarExtender0" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtSaleDate"></cc1:CalendarExtender>

                               

                                        <asp:Label ID="Label18" runat="server" CssClass=" smLbl_to" Text="Sale Amt:"></asp:Label>
                                        <asp:TextBox ID="txtsaleamt"  runat="server" CssClass="inputtextbox" Style="text-align:right;"></asp:TextBox>
                                     
                                         <asp:Label ID="lblserialno" runat="server" Visible="False"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-12  pading5px  asitCol10">
                                        <asp:Label ID="Label19" runat="server" CssClass="lblName lblTxt" Text="Department:"></asp:Label>

                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="ddlPage" Width="205px">
                                        </asp:DropDownList>
                                        <asp:Label ID="Label20" runat="server" CssClass="lblName lblTxt" Text="Client Name:"></asp:Label>
                                        <asp:TextBox ID="txtClient" Width="210px" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-10  pading5px  asitCol10">
                                        <asp:Label ID="Label21" runat="server" CssClass="lblName lblTxt" Text="Executive:"></asp:Label>
                                        <asp:TextBox ID="txtExecutive" Width="200" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:Label ID="lmsg" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lblAddToTable" Width="100"  runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lblAddToTable_Click" TabIndex="3">Add To Table</asp:LinkButton>
                                    </div>
                                </div>
                            </asp:Panel>
                                
                             </div>
                             </fieldset>

                            <div class="table table-responsive">
                                <asp:GridView ID="gvSalReg" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="478px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvsalesno" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "salesno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Name">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbTotal" runat="server" Font-Bold="True" Font-Size="12px" CssClass="btn primaryBtn"
                                                    ForeColor="#000" OnClick="lbTotal_Click" Style="text-decoration: none;"> Total </asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvunitname" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sale Date">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnFinalUpdate" runat="server"  OnClick="lbtnFinalUpdate_Click" CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvsaldate" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "saledate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvsaldate_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvsaldate"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="left" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sale Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="txtFTotal" runat="server" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvsalamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saleamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                                VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDepartment" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvcustname" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))%>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Executive Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvexecutive" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "executive")) %>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
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

                        <asp:View ID="View2" runat="server">
                             <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                              <asp:Panel ID="Panel3" runat="server"  Style="margin:8px;">
                                   <div class="form-group">
                                    <div class="col-md-10  pading5px  asitCol10">
                                        <asp:Label ID="Label22" runat="server" CssClass="lblName lblTxt"  Text="Sales No:"></asp:Label>

                                        <asp:TextBox ID="txtSrcSalesNo" runat="server" CssClass="inputtextbox" ></asp:TextBox>

                                        <asp:LinkButton ID="ibtnFindSalesNo" runat="server" OnClick="ibtnFindSalesNo_Click" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                          <asp:Label ID="lblmsg02" runat="server" CssClass="btn-danger btn disabled" Visible="false" ></asp:Label>

                                    </div>
                                </div>
                              </asp:Panel>
                                </div>
                               </fieldset>
                            <div class="table table-responsive">
                                <asp:GridView ID="gvSalRegEdit" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="478px" AllowPaging="True"
                                    OnPageIndexChanging="gvSalRegEdit_PageIndexChanging"
                                    OnRowCancelingEdit="gvSalRegEdit_RowCancelingEdit"
                                    OnRowDeleting="gvSalRegEdit_RowDeleting" OnRowEditing="gvSalRegEdit_RowEditing"
                                    OnRowUpdating="gvSalRegEdit_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:CommandField ShowEditButton="True" />
                                        <asp:TemplateField HeaderText="Sales No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvsalesnoedit" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "salesno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project  Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvprojectname" runat="server" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Unit Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvunitnameedit" runat="server" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="70px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sale Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvsaldateedit" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "saledate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvsaldateedit_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvsaldateedit"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="left" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sale Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="txtFTotaledit" runat="server" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvsalamtedit" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saleamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                                VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <EditItemTemplate>
                                                <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                                    BorderWidth="1px" Height="29px" Width="318px">
                                                    <table style="width: 63%; height: 20px;">
                                                        <tr>
                                                            <td class="style104">
                                                                <asp:TextBox ID="txtDeptSearch" runat="server" BorderStyle="Solid"
                                                                    BorderWidth="1px" Height="18px" TabIndex="15" Width="86px"></asp:TextBox>
                                                            </td>
                                                            <td class="style105">
                                                                <asp:ImageButton ID="ibtnSrchDept" runat="server" Height="16px"
                                                                    ImageUrl="~/Image/find_images.jpg" OnClick="ibtnSrchDept_Click" TabIndex="16"
                                                                    Width="16px" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDeptName" runat="server" TabIndex="17" Width="200px">
                                                                </asp:DropDownList>
                                                                <cc1:ListSearchExtender ID="ddlDeptName_ListSearchExtender" runat="server"
                                                                    Enabled="True" QueryPattern="Contains" TargetControlID="ddlDeptName">
                                                                </cc1:ListSearchExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDepartmentedit" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvcustnameedit" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))%>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Executive Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvexecutiveedit" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "executive")) %>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
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

            </div>
        



            <%--<asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                    BorderWidth="1px">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label15" runat="server" CssClass="style29" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" Text="Project Name:" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style91">
                                                <asp:TextBox ID="txtSrcPro" runat="server" BorderStyle="None"
                                                    CssClass="txtboxformat" TabIndex="0" Width="80px"></asp:TextBox>
                                            </td>
                                            <td class="style28">
                                                <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px"
                                                    ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindProject_Click"
                                                    TabIndex="1" />
                                            </td>
                                            <td class="style61" valign="top">
                                                <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True"
                                                    Font-Size="12px" TabIndex="2" Width="350px">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblProjectdesc" runat="server" BackColor="#000"
                                                    Font-Size="12px" ForeColor="Blue" Height="16px" Visible="False" Width="350px"></asp:Label>
                                                <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366"
                                                    BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" OnClick="lbtnOk_Click" TabIndex="3">Ok</asp:LinkButton>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="style101">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>--%>

            <%--<tr>
                                            <td class="style92">
                                                <asp:Label ID="Label16" runat="server" CssClass="style29" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" Text="Unit Name:" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style93">
                                                <asp:TextBox ID="txtUnitName" runat="server" BorderStyle="None"
                                                    CssClass="txtboxformat" TabIndex="4" Width="200px"></asp:TextBox>
                                            </td>
                                            <td class="style102">
                                                <asp:Label ID="Label17" runat="server" CssClass="style29" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" Text="Sale Date:" Width="70px"></asp:Label>
                                            </td>
                                            <td class="style99">
                                                <asp:TextBox ID="txtSaleDate" runat="server" AutoCompleteType="Disabled"
                                                    BorderStyle="None" BorderWidth="1px" TabIndex="5" Width="80px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtSaleDate_CalendarExtender0" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtSaleDate"></cc1:CalendarExtender>
                                            </td>
                                            <td class="style96">
                                                <asp:Label ID="Label18" runat="server" CssClass="style29" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" Text="Sale Amt:" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style98">
                                                <asp:TextBox ID="txtsaleamt" runat="server" BorderStyle="None"
                                                    CssClass="txtboxformat" TabIndex="6" Width="80px"></asp:TextBox>
                                            </td>
                                            <td class="style103">
                                                <asp:Label ID="lblserialno" runat="server" Visible="False"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>--%>

            <%--<tr>
                                            <td class="style92">
                                                <asp:Label ID="Label19" runat="server" CssClass="style29" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" Text="Department:" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style93">
                                                <asp:DropDownList ID="ddlDepartment" runat="server" Font-Bold="True"
                                                    Font-Size="12px" TabIndex="7" Width="205px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style102">
                                                <asp:Label ID="Label20" runat="server" CssClass="style29" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" Text="Client Name:" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style97" colspan="3">
                                                <asp:TextBox ID="txtClient" runat="server" BorderStyle="None"
                                                    CssClass="txtboxformat" TabIndex="8" Width="255px"></asp:TextBox>
                                            </td>
                                            <td class="style103">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>--%>
            <%--<tr>
                                            <td class="style92">
                                                <asp:Label ID="Label21" runat="server" CssClass="style29" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" Text="Executive:" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style93">
                                                <asp:TextBox ID="txtExecutive" runat="server" BorderStyle="None"
                                                    CssClass="txtboxformat" TabIndex="9" Width="200px"></asp:TextBox>
                                            </td>
                                            <td class="style102">&nbsp;</td>
                                            <td class="style97" colspan="3">
                                                <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" Style="color: #FFFFFF"></asp:Label>
                                            </td>
                                            <td class="style103">
                                                <asp:LinkButton ID="lblAddToTable" runat="server" BackColor="#003366"
                                                    BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" ForeColor="#000"
                                                    OnClick="lblAddToTable_Click" Style="font-size: small" TabIndex="10"
                                                    Width="80px">Add To Table</asp:LinkButton>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>--%>
                    
            <%--<tr>
                        <td>
                            <asp:Label ID="Label22" runat="server" CssClass="style29" Font-Bold="True"
                                Font-Size="12px" ForeColor="#000" Text="Sales No:" Width="80px"></asp:Label>
                        </td>
                        <td class="style91">
                            <asp:TextBox ID="txtSrcSalesNo" runat="server" BorderStyle="None"
                                CssClass="txtboxformat" TabIndex="11" Width="80px"></asp:TextBox>
                        </td>
                        <td class="style28">
                            <asp:ImageButton ID="ibtnFindSalesNo" runat="server" Height="18px"
                                ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindSalesNo_Click"
                                TabIndex="12" Style="width: 18px" />
                        </td>
                        <td class="style61" valign="top">
                            <asp:Label ID="lblmsg02" runat="server" BackColor="Red" Font-Bold="True"
                                Font-Size="12px" ForeColor="#000" Style="color: #FFFFFF"></asp:Label>
                        </td>
                      
                    </tr>--%>
          





        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

