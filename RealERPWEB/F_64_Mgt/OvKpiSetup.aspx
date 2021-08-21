
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="OvKpiSetup.aspx.cs" Inherits="RealERPWEB.F_64_Mgt.OvKpiSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });


        function pageLoaded() {

            var gvSetupDet = $('#<%=this.gvSetupDet.ClientID%>');
            gvSetupDet.Scrollable();



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


                                    <div class="col-md-12 pading5px ">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Information :</asp:Label>
                                        <asp:TextBox ID="txtDesc" runat="server" CssClass=" inputTxt lblName txtAlgLeft" Font-Bold="true" Font-Size="14px" Width="606px"></asp:TextBox>

                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass=" lblTxt lblName">Page Size :</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass=" ddlPage">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>


                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:CheckBox ID="chkAllSInf" runat="server" AutoPostBack="True" CssClass=" smLbl_to" Width="93px"
                                            OnCheckedChanged="chkAllSInf_CheckedChanged" Text="Show All" />

                                        <asp:CheckBox ID="Chkcopy" runat="server" AutoPostBack="True" CssClass=" smLbl_to" Width="93px"
                                            OnCheckedChanged="Chkcopy_CheckedChanged" Text="Copy Data" />

                                    </div>

                                    <div class="col-md-2 pading5px">
                                        <div class="colMdbtn pading5px">
                                            <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="False" OnDataBinding="lmsg_DataBinding"></asp:Label>

                                        </div>



                                    </div>
                                </div>

                                <asp:Panel ID="pnlCopy" runat="server" Visible="false" Style="border: 1px solid blue;">

                                    <div class="form-group">
                                        <div class="col-md-5 pading5px ">
                                            <asp:Label ID="lblPrevious" runat="server" CssClass=" smLbl_to" Text="Month:"></asp:Label>

                                            <asp:DropDownList ID="ddlperyearmon" runat="server" 
                                                TabIndex="11" CssClass="ddlPage">
                                            </asp:DropDownList>

                                            <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="lbtnCopy" runat="server" Text="Copy" OnClick="lbtnCopy_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                                            </div>


                                        </div>

                                    </div>

                                </asp:Panel>

                            </div>
                        </fieldset>
                    </div>



                    <asp:GridView ID="gvSetupDet" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        OnPageIndexChanging="gvSetupDet_PageIndexChanging" ShowFooter="True" OnRowDeleting="gvSetupDet_RowDeleting">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />

                            <asp:TemplateField HeaderText="Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvActcode" runat="server"
                                        ForeColor="Black" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work List">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lFinalUpdate_Click">Final Update</asp:LinkButton>

                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvActivi" runat="server"
                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                        Width="450px">





                                    </asp:Label>

                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty of </br> Work">

                                <FooterTemplate>
                                    <asp:LinkButton ID="lblgvFTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lblgvFTotal_Click">Total</asp:LinkButton>
                                </FooterTemplate>

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvwqty" runat="server" BackColor="Transparent"
                                        BorderStyle="None"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wqty")).ToString("#,##0;-#,##0; ")%>'
                                        Width="50px" Font-Size="11px" Style="text-align: right"></asp:TextBox>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Marks">

                                <FooterTemplate>
                                    <asp:Label ID="lblgvFmarks" runat="server"></asp:Label>
                                </FooterTemplate>

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvmarks" runat="server" BackColor="Transparent"
                                        BorderStyle="None"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "marks")).ToString("#,##0.00;-#,##0.00; ")%>'
                                        Width="50px" Font-Size="11px" Style="text-align: right"></asp:TextBox>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkvmrno" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "active"))=="True" %>'
                                        Width="50px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

