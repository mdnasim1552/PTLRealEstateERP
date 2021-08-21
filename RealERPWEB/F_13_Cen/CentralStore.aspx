<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CentralStore.aspx.cs" Inherits="RealERPWEB.F_13_Cen.CentralStore" %>

<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="Date From"></asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <asp:Label ID="lbldateto" runat="server" CssClass="lblTxt lblName" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <cc1:CalendarExtender ID="Calfr" runat="server" Format="dd-MMM-yyyy "
                                            TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="Calto" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtDateto"></cc1:CalendarExtender>

                                    </div>
                                </div>


                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName" Text="Store"></asp:Label>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ImgbtnFindProj" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProj_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlAccProject" OnSelectedIndexChanged="ddlAccProject_SelectedIndexChanged" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4">ok</asp:LinkButton>

                                    </div>


                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Branch Name"></asp:Label>
                                        <asp:TextBox ID="txtseachbranch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ImgbtnFindBrach" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindBrach_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>


                                    <asp:Panel ID="Panel2" runat="server" Width="400" Height="25px">
                                        <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" CssClass="form-control inputTxt pull-left"
                                            MaxDropDownHeight="200" TabIndex="8" TransitionalMode="True" Width="400px">
                                        </cc1:DropCheck>
                                        <div class="clearfix"></div>
                                    </asp:Panel>



                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblRptGroup" runat="server" CssClass="lblTxt lblName" Text="Group"></asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server"
                                            OnSelectedIndexChanged="ddlRptGroup_SelectedIndexChanged" CssClass="ddlPage">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:Label ID="lblPage0" runat="server" CssClass=" smLbl_to" Text="Page Size"></asp:Label>


                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <asp:GridView ID="gvCenStore" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvCenStore_PageIndexChanging"
                                ShowFooter="True" Width="628px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Store Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProjectName" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "pactdesc").ToString() %>'
                                                Width="250px" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Materials Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMatdescryption" runat="server"
                                                Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                                Width="220px">
                                                    
                                                    
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUnit0" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "rsirunit").ToString() %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOpQty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Opening Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOpratey" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Opening Amt.">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFOpnAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOpAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Stock Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStQty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStRate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFStkAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:View>
                        <asp:View ID="ViewMStorewlsd" runat="server">
                            <asp:GridView ID="gvCenStorewlsd" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnPageIndexChanging="gvCenStorewlsd_PageIndexChanging" ShowFooter="True"
                                Width="628px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Store Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProjectNameqb" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "pactdesc").ToString() %>'
                                                Width="250px" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Materials Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMatdescryptionlsd" runat="server"
                                                Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                                Width="220px">
                                                    
                                                    
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUnitlsd" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "rsirunit").ToString() %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOpQtylsd" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Purchase">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvReQtylsd" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Transfer In">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTrninqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tinqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Issue">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvissueqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer Out">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTrnoutqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toutqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Lost ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlostqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Sold ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsoldqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Destroyed ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdesqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Closing Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStQty0" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:View>
                        <asp:View ID="ViewStkAmount" runat="server">
                            <asp:GridView ID="gvMatSAbasis" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnPageIndexChanging="gvMatSAbasis_PageIndexChanging" ShowFooter="True"
                                Width="628px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo2" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Store Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProjectNameab" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "pactdesc").ToString() %>'
                                                Width="250px" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Materials Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMatdescryptionab" runat="server"
                                                Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                                Width="220px">
                                                    
                                                    
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUnitab" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "rsirunit").ToString() %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOpnamab" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFOpnAmtab" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purchase">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpuramab" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFpuramab" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />

                                        <ItemStyle HorizontalAlign="Right" />

                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer In">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtinamab" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tinam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtinamab" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvissueamab" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFissueamab" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Transfer Out">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtrnoutam" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toutam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtrnoutam" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lost ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlostam" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFlostam" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />


                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sold ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsoldam" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFsoldam" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />


                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Destroyed ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdesam" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dam")).ToString("#,##0;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFdesam" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />


                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvstkamab" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFstkamab" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

