<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptDeptWEmpPendActivities.aspx.cs" Inherits="RealERPWEB.F_62_Mis.RptDeptWEmpPendActivities" %>

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

           
          // Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded();



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
                                    <div class="col-md-3 pading5px  asitCol3">

                                        <asp:Label ID="lblDepartment" runat="server" CssClass=" lblTxt lblName" Text="Department"> </asp:Label>
                                        <asp:TextBox ID="txtdepartment" runat="server" TabIndex="3" CssClass="inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgsrchdepartment" runat="server" TabIndex="4" CssClass="btn btn-primary srearchBtn" OnClick="imgsrchdepartment_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <%--  <button id="imgSearchSalesTeam" onclick="javascript:SearchSalesTeam()"  tabindex="4"  class="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></button>--%>
                                        </div>

                                    </div>


                                    <div class="col-md-3 pading5px  asitCol3">

                                        <asp:DropDownList ID="ddldepartment" runat="server" 
                                            TabIndex="11" CssClass=" ddlPage" style=" width:180px;">
                                        </asp:DropDownList>
                                           <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDatefrm" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>

                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate">
                                        </cc1:CalendarExtender>

                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to" Text="To"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
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

                         

                            </div>
                        </fieldset>


                    </div>
                  
                            <asp:GridView ID="gvPenWork" runat="server"
                                AutoGenerateColumns="False" ShowFooter="true"
                                CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlgen" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1).Trim()+"." %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="28" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                      <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvempname" runat="server"
                                                Text='<%# (DataBinder.Eval(Container.DataItem,"empname")) %>'
                                                Width="150px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                   

                                      <asp:TemplateField HeaderText="Taget <br/>Setup">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtsetgen" runat="server"
                                                Text='<%# (DataBinder.Eval(Container.DataItem,"tarmon")) %>'
                                                Width="80px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Activities">
                                        <ItemTemplate>
                                            <asp:Label ID="lblactivitiesgen" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"actdesc")) %>'
                                                Width="300px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>






                                    <asp:TemplateField HeaderText="Pending ">
                                   
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"bal")).ToString("#,##0;(#,##0);") %>'
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

