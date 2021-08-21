<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptFxtStore.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.RptFxtStore" %>

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
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="lblProjectname" CssClass="lblTxt lblName" runat="server" Text="Store"></asp:Label>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindProj" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindProj_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlAccProject" runat="server" OnSelectedIndexChanged="ddlAccProject_SelectedIndexChanged" CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true">
                                        </asp:DropDownList>

                                    </div>


                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>
                                    <div class="clearfix"></div>

                                </div>

                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="Label3" CssClass="lblTxt lblName" runat="server" Text="Branch Name"></asp:Label>
                                        <asp:TextBox ID="txtseachbranch" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindBrach" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindBrach_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <cc1:DropCheck ID="DropCheck1" runat="server" MaxDropDownHeight="200" TabIndex="8" TransitionalMode="True" Width="400px" >
                                    </cc1:DropCheck>



                                    <div class="clearfix"></div>

                                </div>

                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol4">

                                        <asp:Label ID="lblDatefrom" CssClass="lblTxt lblName" runat="server" Text="Date"></asp:Label>

                                        <asp:TextBox ID="txtDatefrom" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy "
                                            TargetControlID="txtDatefrom"></cc1:CalendarExtender>


                                        <asp:Label ID="lbldateto" CssClass="smLbl_to" runat="server" Text="To"></asp:Label>

                                        <asp:TextBox ID="txtDateto" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                        <cc1:CalendarExtender ID="Calto" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtDateto"></cc1:CalendarExtender>
                                    </div>
                                    <div class=" col-md-3  pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page" Visible="False"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            CssClass=" ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>  
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    
                                    <div class="clearfix"></div>

                                </div>

                            </div>
                        
                        </fieldset>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewMStorewlsd" runat="server">
                            <div  class="row table-responsive">
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
                                </div>
                        </asp:View>
                        <asp:View ID="ViewStkAmount" runat="server">
                            <div class="row table-responsive">
                            <asp:GridView ID="gvMatSAbasis" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False"
                                CssClass="  table-responsive table-striped table-hover table-bordered grvContentarea"
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
                            </div>
                        </asp:View>
                    </asp:MultiView>

                    </div>
                </div>
            






            

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


