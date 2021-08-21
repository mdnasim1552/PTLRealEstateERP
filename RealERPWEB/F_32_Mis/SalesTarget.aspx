<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SalesTarget.aspx.cs" Inherits="RealERPWEB.F_32_Mis.SalesTarget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
         <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            try {


                $('#<%=this.gvSalbgd.ClientID%>').tblScrollable();
                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e) {
                alert(e);
            }

        };
         </script>
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>



            <div class="container moduleItemWrpper">
                <div class="contentPart">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">

                                        <div class="form-group">

                                            <div class="col-md-2 pading5px">
                                                <asp:Label ID="lCurAppdate" runat="server" CssClass="lblTxt lblName" Text="Month"></asp:Label>
                                                <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True" CssClass="ddlPage">
                                                </asp:DropDownList>
                                               
                                            </div>
                                            <div class="col-md-4 pading5px">
                                                <asp:Label ID="Empname" runat="server" CssClass="lblTxt lblName" Text="Employee"></asp:Label>
                                                <asp:Label ID="loademp" runat="server" style="padding-top: 3px;" BorderStyle="None" BorderWidth="1px">
                                                </asp:Label>
                                            </div>
                                            <%--<div class="col-md-1 pading5px" style="margin-left: -80px;">
                                                 <asp:LinkButton ID="lbtnOk" runat="server"  CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>
                                            </div>--%>
                                            <div class="col-md-3">
                                                <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <asp:GridView ID="gvSalbgd" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="531px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbTotal_OnClick" > Total </asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnFinalUpdate"
                                                CssClass="btn btn-danger primaryBtn" runat="server" OnClick="lbtnFinalUpdate_OnClick">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lblgvDepartment" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="180px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Sales Target">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvsalamt" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saltg")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFsaltg" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    
                                     <asp:TemplateField HeaderText="Total Coll. Target">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvcolamt" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coltg")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                            <asp:Label ID="lgvFcoltg" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                         <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
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
            </div>


            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


