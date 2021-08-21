<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkFxtAssetDetails.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.LinkFxtAssetDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
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


    <div class="container moduleItemWrpper">
        <div class="contentPart">


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-9 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Description :</asp:Label>

                                        <asp:TextBox ID="lblrsirdesc" runat="server" CssClass=" inputlblVal" Style="width: 500px;"></asp:TextBox>

                                         <asp:Label ID="lblDept" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                         <asp:DropDownList ID="ddlProjectName" runat="server" Width="180" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" CssClass=" ddlPage chzn-select" TabIndex="6">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblDeptDesc" runat="server" CssClass="smLbl_to" Visible="False" Width="233px"></asp:Label>

                                    </div>

                                 

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="LinkButton1_Click">Add</asp:LinkButton>
                                    </div>
                                    <div class="colMdbtn pading5px">
                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>

                                    </div>



                                </div>

                            </div>
                    </div>
                    </fieldset>
                  
                    <asp:GridView ID="gvProSlInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" OnRowEditing="gvProSlInfo_RowEditing" OnRowUpdating="gvProSlInfo_RowUpdating" OnRowCancelingEdit="gvProSlInfo_RowCancelingEdit"
                        OnRowDataBound="gvProSlInfo_RowDataBound">
                        <RowStyle />
                        <Columns>


                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dept#" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgpactcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Serial#">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lgcSlCode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slno")) %>'
                                        Width="40px"></asp:HyperLink>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Asset id </br> code">
                                <ItemTemplate>
                                    <asp:TextBox ID="lblidcode" runat="server" BorderStyle="none"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assetidcode")) %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Model#">
                                <ItemTemplate>
                                    <asp:TextBox ID="lblmodel" runat="server" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "modelno")) %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lgpactdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="150px"></asp:HyperLink>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Floor">
                                <ItemTemplate>

                                    <asp:TextBox ID="txtfloor" runat="server" BorderStyle="none"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "floorno")) %>'
                                        Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="TblTotal" runat="server" Visible="false" CssClass="btn btn-primary primaryBtn" OnClick="TblTotal_Click">Total</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>



                            <asp:CommandField ShowEditButton="True" />
                            <asp:TemplateField HeaderText=" User Name">

                                <EditItemTemplate>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <table style="width: 100%;">
                                            <tr>
                                               
                                                <td>
                                                    <asp:DropDownList ID="ddlemp" runat="server" CssClass="ddlistPull chzn-select" Width="200px" TabIndex="6">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </EditItemTemplate>

                                <ItemTemplate>

                                    <asp:Label ID="lblusrname" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                        Width="170px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdate_Click1">Update</asp:LinkButton>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lbldig" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>






                            <asp:TemplateField Visible="false" HeaderText="Date of</br>Purchase">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvdate" runat="server" BorderStyle="none"
                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "purchasedate"))%>'
                                        Width="70px"></asp:TextBox>



                                    <cc1:CalendarExtender ID="txtgvdate_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtgvdate"></cc1:CalendarExtender>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estimated Life" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="lblestimated" runat="server" BorderStyle="none"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "estimatedlife")) %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Category" Visible="false">

                                <EditItemTemplate>
                                    <%--  <asp:Panel ID="Panel2" runat="server" Visible="false">--%>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtSrchCate" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                            </td>
                                            <td>
                                                <asp:LinkButton ID="ibtnSrchcate" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="ddlistPull" Width="100px" TabIndex="6">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--    </asp:Panel>--%>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblcategory" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "category")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Purchase Price" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtpurprice" runat="server" BorderStyle="none" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purchaseprice")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Rate of Depreciation" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtrate" runat="server" BorderStyle="none"
                                        Text='<%#(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ratedepreciation"))==0.00)? Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ratedepreciation")).ToString("#,##0.00;(#,##0.00); "): Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ratedepreciation")).ToString("#,##0.00;(#,##0.00); ") + "%"  %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Accumulated  Depreciation" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAccmulated" runat="server" BorderStyle="none" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "accudepreciation")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date of Depreciation" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvdepre" runat="server" BorderStyle="none" Style="text-align: center"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "depreciationdate")) %>'
                                        Width="80px"></asp:TextBox>

                                    <cc1:CalendarExtender ID="txtgvdepre_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtgvdepre"></cc1:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--   <asp:TemplateField HeaderText="Current year Depreciation">
                                <ItemTemplate>
                                    <asp:Label ID="txtcurrent" runat="server" BorderStyle="none"  style="text-align:right"  
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "currentyear")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Adjustment /Disposal" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtadjustment" runat="server" BorderStyle="none" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjustment")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- <asp:TemplateField HeaderText="Wdv">
                                <ItemTemplate>
                                    <asp:Label ID="txtwdv" runat="server" BorderStyle="none" style="text-align:right"   
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wdv")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>--%>


                            <asp:TemplateField HeaderText="Warranty" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="lblwarranty" runat="server" BorderStyle="none"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "warranty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="lblqy" runat="server" BorderStyle="none"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0") %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="lblremarks" runat="server" BorderStyle="none"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                        Width="120px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnde" runat="server" OnClick="btnde_click"><span style="color:red"  class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="30px" />
                                <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                    </div>

                

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>


