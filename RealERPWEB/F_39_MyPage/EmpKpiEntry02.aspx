<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EmpKpiEntry02.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.EmpKpiEntry02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../js/bootstrap.js"></script>
    <script src="../Scripts/ScrollableGridPlugin.js"></script>
    <script src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript">
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
            <div class="row">

                <fieldset class="scheduler-border fieldset_B">

                    <div class="form-horizontal">


                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblDatefrm" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>

                                <asp:TextBox ID="txtdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate">
                                </cc1:CalendarExtender>

                            </div>


                            <div class="col-md-2 pading5px asitCol3">
                                <div class="msgHandSt">
                                    <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                </div>

                            </div>



                        </div>

                        <div class="form-group">

                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblSalesTeam" runat="server" CssClass="lblTxt lblName" Text="Employee Name"></asp:Label>
                                <asp:TextBox ID="txtSrchSalesTeam" runat="server" TabIndex="3" CssClass=" inputtextbox"></asp:TextBox>
                                <div class="colMdbtn">
                                    <asp:LinkButton ID="imgSearchSalesTeam" runat="server" OnClick="imgSearchSalesTeam_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                </div>
                            </div>



                            <div class="col-md-4 pading5px">
                                <asp:DropDownList ID="ddlEmpid" runat="server" CssClass=" form-control inputTxt"
                                    TabIndex="5">
                                </asp:DropDownList>
                            </div>


                            <div class="col-md-1 pading5px">
                                <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>


                            </div>


                        </div>
                       
                    </div>
                </fieldset>

               

                <asp:GridView ID="gvempkpi" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="668px" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvempkpi_RowDataBound" OnRowCreated="gvempkpi_RowCreated" >
                            <RowStyle />
                            <Columns>
                              
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                             




                                <asp:TemplateField HeaderText="Activities">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtxtTotal" runat="server" Text="Total Days"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActivi" runat="server"
                                              Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'  

                                            Width="400px" Font-Size="11px">





                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                               
                                
                                <asp:TemplateField HeaderText="Duration">

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFTotal" runat="server"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvduration" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;-#,##0; ")%>'
                                            Width="50px" Font-Size="11px" style="text-align:right"></asp:TextBox>
                                    </ItemTemplate>
                                   

                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                             
                                
                                
                                <asp:TemplateField HeaderText="Start ">

                                    <ItemTemplate>
                                        <asp:Label ID="lblStDate" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tstdat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tstdat")).ToString("dd-MMM-yyyy")%>'
                                            Width="65px" Font-Size="11px"></asp:Label>
                                       
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primarygrdBtn"
                                             OnClick="lbtnUpdate_Click"
                                            >Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Finish">

                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tenddat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tenddat")).ToString("dd-MMM-yyyy")%>'
                                            Width="65px" Font-Size="11px"></asp:Label>
                                     
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Start">

                                    <ItemTemplate>
                                         <asp:TextBox ID="txtacstDate" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acstdat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acstdat")).ToString("dd-MMM-yyyy")%>'
                                            Width="65px" Font-Size="11px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtacstDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtacstDate">
                                        </cc1:CalendarExtender>
                                     
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Finish">

                                    <ItemTemplate>
                                         <asp:TextBox ID="txtacDate" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acenddat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acenddat")).ToString("dd-MMM-yyyy")%>'
                                            Width="65px" Font-Size="11px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtacDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtacDate">
                                        </cc1:CalendarExtender>
                                     
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                  <asp:TemplateField HeaderText="Comments">
                                       
                                      
                                        <ItemTemplate>



                                            <asp:HyperLink ID="hlnkgvcomments" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: left; background-color: Transparent; color: blue;"
                                                Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comments"))  %>'
                                                Width="80px">                                             
                                            
                                            </asp:HyperLink>

                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
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

