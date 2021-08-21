<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="TASSchduleRate.aspx.cs" Inherits="RealERPWEB.F_07_Ten.TASSchduleRate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            
            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="row">
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">

                            <div class="form-group">

                                <div class="col-md-6 pading5px">
                                    <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Item:"></asp:Label>

                                    <asp:TextBox ID="txtSrcItem" runat="server" CssClass="inputTxt inpPixedWidth"
                                        Width="80px"></asp:TextBox>


                                    <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_OnClick" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    <asp:DropDownList ID="ddlItem" runat="server" Font-Bold="True" Font-Size="12px"
                                        Width="300px" CssClass="chzn-select form-control inputTxt">
                                    </asp:DropDownList>
                                    <%--<asp:Label ID="lblItem" runat="server" BackColor="White" Font-Bold="True"
                                        Font-Size="12px" ForeColor="Blue" Visible="False" Width="300px"></asp:Label>--%>
                                    <asp:Label ID="lblItem"  Width="300px" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>

                                </div>
                                <div class="col-md-1 pading5px">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click" TabIndex="4" Style="margin-left: -70px;">Ok</asp:LinkButton>
                               <%-- <asp:CheckBox ID="chkShorting" runat="server" AutoPostBack="true" Text="Alphabet" />--%>

                            </div>
                            
                            <div class="col-md-2">
                                <asp:Label ID="lblmsg1" runat="server" BackColor="Red" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label>
                            </div>

                            </div>

                            



                        </div>


                    </fieldset>

                </div>
                <div class="row">
                    <asp:GridView ID="gvschrate" runat="server" AutoGenerateColumns="False"
                                 ShowFooter="True"
                                Style="text-align: left" Width="650px" CssClass="table-responsive table-striped table-hover table-bordered grvContentarea">
                    <PagerSettings Position="Top" />
                    <RowStyle  Font-Size="11px" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Floor Description">
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnFinalUpdate" runat="server" Font-Bold="True"
                                    Font-Size="12px" ForeColor="Blue" OnClick="lbtnFinalUpdate_Click">Final Update</asp:LinkButton>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lgvfloordesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                    Width="100px"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Font-Bold="False" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" Schdule Rate(1)">
                            <ItemTemplate>

                                <asp:TextBox ID="txtgvschrate1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrate1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText=" Schdule Rate(2)">
                            <ItemTemplate>

                                <asp:TextBox ID="txtgvschrate2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrate2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
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
