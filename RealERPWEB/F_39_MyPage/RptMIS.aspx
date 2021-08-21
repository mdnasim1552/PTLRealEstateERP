
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMIS.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.RptMIS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
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

                                    <div class="col-md-6 pading5px ">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate">
                                        </cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" Text="To" TabIndex="1" CssClass=" smLbl_to"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox" TabIndex="2"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to " Text="Page Size"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            CssClass="ddlPage62 inputTxt">
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
                                        <div class=" colMdbtn pading5px">
                                            <asp:LinkButton ID="lbntOk" runat="server" Text="Ok" OnClick="lbntOk_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>

                                        </div>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                    </div>
                                    <div class="col-md-1 pading5px">
                                    </div>


                                    <div class="clearfix"></div>


                                </div>
                            </div>
                        </fieldset>

                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="evaluationpwise" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvevaoproject" runat="server" AllowPaging="True" OnPageIndexChanging="gvevaoproject_PageIndexChanging"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                        CssClass="table table-striped table-hover table-bordered grvContentarea" Width="500px" OnRowCreated="gvevaoproject_RowCreated" OnRowDataBound="gvevaoproject_RowDataBound">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNotd" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvprojectname" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                        Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="300px">

                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Start">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvstdate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tstdat")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Completion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvenddate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tenddat")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText=" Target Completion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvterpertd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tper")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Actual Completion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacpertd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aper")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                        </Columns>

                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>

                                </div>
                            </asp:View>
                            <asp:View ID="ViewEmpEva" runat="server">

                                <asp:GridView ID="gvEmpEval" runat="server" AllowPaging="True" Width="200px"
                                    AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                    CssClass="table table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvEmpEval_RowDataBound" OnPageIndexChanging="gvEmpEval_PageIndexChanging">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="5px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Empid" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"empid")) %>'
                                                    Width="100px"
                                                    Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>

                                                 <asp:HyperLink ID="hlnkempname" runat="server" Target="_blank"
                                       
                                                     Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) 
                                                     
                                                    
                                                                         
                                                                    %>'
                                        Width="150px"
                                        Style="text-align: left" ForeColor="#155273" BackColor="Transparent" BorderStyle="None" Font-Size="13px" ></asp:HyperLink>



                                               
                                           
                                                
                                                
                                                 </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Month Target">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtar" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tar")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFtar" runat="server" Style="text-align: right; color: black;" Width="60px"></asp:Label></FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                          <asp:TemplateField HeaderText="Cum. Target">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcumtar" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"cumtar")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFcumtar" runat="server" Style="text-align: right; color: black;" Width="60px"></asp:Label></FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText=" Month Actual">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvact" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"act")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFact" runat="server" Style="text-align: right; color: black;" Width="60px"></asp:Label></FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText=" Cum. Actual">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcumact" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"cumact")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFcumact" runat="server" Style="text-align: right; color: black;" Width="60px"></asp:Label></FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Result">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltpar" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tper")).ToString("#,##0.00;(#,##0.00); ")+"</B>" %>'
                                                    Width="55px"
                                                    Style="text-align: right" BackColor="Transparent" Font-Size="13px" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Grade">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnGpa" runat="server"
                                                    Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem,"gpa"))+"</B>" %>'
                                                    Width="100px"
                                                    Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="13px" OnClick="btnGpa_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Graph">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnGraph" runat="server"
                                                    Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem,"graph"))+"</B>" %>'
                                                    Width="50px"
                                                    Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="13px" OnClick="btnGraph_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>

                            <asp:View ID="ViewEmpHistory" runat="server">
                                <asp:GridView ID="gvemphis" runat="server" AllowPaging="True" OnPageIndexChanging="gvemphis_PageIndexChanging"
                                    AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                    CssClass="table table-striped table-hover table-bordered grvContentarea" Width="500px">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNohis" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempnamehis" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="150px" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Activities">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempnamehis" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Start Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtstdat" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tstdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Finishing Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtenddat" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tenddat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Comments Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdisdate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "disdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Comments">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempnamehis" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discuss")) %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                    </Columns>

                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>


                            <asp:View ID="ViewindEmpHistory" runat="server">

                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">


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
                                            </div>
                                            <div class="col-md-1 pading5px">
                                                <asp:Label ID="lbluseid" runat="server" CssClass="lblTxt lblName" Style="display: none;"></asp:Label>


                                            </div>


                                        </div>

                                    </div>
                                </fieldset>


                                <asp:GridView ID="gvindemphis" runat="server"
                                    AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                    CssClass="table table-striped table-hover table-bordered grvContentarea" Width="500px" OnRowCreated="gvindemphis_RowCreated" OnRowDataBound="gvindemphis_RowDataBound">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoinhis" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Activities">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvactdescinhis" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Start Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtstdatiemphis" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tstdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Finishing Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtenddatiemphis" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tenddat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Actual Start Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvacstdatiemphis" runat="server"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acstdat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acstdat")).ToString("dd-MMM-yyyy")%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Actual End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvacenddatiemphis" runat="server"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acenddat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acenddat")).ToString("dd-MMM-yyyy")%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Delay/Advance">



                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdelayiemphis" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "deloadv")).ToString("#,##0;-#,##0; ")%>'
                                                    Width="50px" Font-Size="11px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>


                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Last Comments Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlastcomdateiemphis" runat="server"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy")%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Comments">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempnamehis" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discuss")) %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                    </Columns>

                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>

                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
