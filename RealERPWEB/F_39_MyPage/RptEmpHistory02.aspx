<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptEmpHistory02.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.RptEmpHistory02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../Content/jquery-ui.css" rel="stylesheet" />

    <script src="../Scripts/Chart.js"></script>
    <script src="../Scripts/Chart.min.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script type="text/javascript">


        $(document).ready(function () {

            $('#<%=this.txtSrchSalesTeam.ClientID%>').focus();



        });


    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDatefrm" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>

                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate">
                                        </cc1:CalendarExtender>

                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to" Text="To"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>

                                    </div>


                                    <div class="col-md-2 pading5px asitCol3">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                        </div>

                                    </div>



                                </div>

                                <div class="form-group">

                                    <div class="col-md-2 pading5px asitCol3">
                                        <asp:Label ID="lblSalesTeam" runat="server" CssClass="lblTxt lblName" Text="Employee Name:"></asp:Label>



                                        <asp:TextBox ID="txtSrchSalesTeam" runat="server" TabIndex="3" CssClass=" inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgSearchSalesTeam" runat="server" OnClick="imgSearchSalesTeam_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <%--  <button id="imgSearchSalesTeam" onclick="javascript:SearchSalesTeam()"  tabindex="4"  class="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></button>--%>
                                        </div>
                                    </div>



                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlEmpid" runat="server" CssClass="ddlPage235 inputTxt"
                                            TabIndex="5">
                                        </asp:DropDownList>
                                    </div>


                                    <div class="col-md-1 pading5px">
                                       
                                         <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" TabIndex="4" CssClass="btn btn-primary  okBtn">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="lbluseid" runat="server" CssClass="lblTxt lblName" Style="display: none;"></asp:Label>


                                    </div>


                                </div>

                            </div>
                        </fieldset>

                        <asp:GridView ID="gvEmpHis" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="668px" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvEmpHis_RowDataBound">
                            <RowStyle />
                            <Columns>
                              
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                               





                                <asp:TemplateField HeaderText="Program #">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpactdesc" runat="server"
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "prono"))      %>'
                                            Width="90px" Font-Size="11px">   
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Reference #">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrefno" runat="server"
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))      %>'
                                            Width="90px" Font-Size="11px">   
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Activities">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActivi" runat="server"
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))
                                                                         
                                                                    %>'
                                            Width="250px" Font-Size="11px">





                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Target<br/>Duration">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvduration" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;-#,##0; ")%>'
                                            Width="50px" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>


                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Actual <br/>Duration">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvaduration" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aduration")).ToString("#,##0;-#,##0; ")%>'
                                            Width="50px" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>


                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Delay">



                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdelay" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "deloadv")).ToString("#,##0;-#,##0; ")%>'
                                            Width="50px" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>


                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>


                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

