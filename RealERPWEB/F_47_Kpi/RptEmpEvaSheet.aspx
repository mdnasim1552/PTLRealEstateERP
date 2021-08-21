<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptEmpEvaSheet.aspx.cs" Inherits="RealERPWEB.F_47_Kpi.RptEmpEvaSheet" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

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
        function loadModal() {
            $('#exampleModal3').modal('show');
        }
        function loadModal4() {
            $('#exampleModal4').modal('show');
        }
        function loadModal5() {
            $('#exampleModal5').modal('show');
        }
        function loadModal6() {
            $('#exampleModal6').modal('show');
        }



        function loadModal_Sales() {
            $('#SalesModal').modal('show');
        }
        function loadModal_Coll() {
            $('#CollectionModal').modal('show');
        }


        function loadModalVisit() {
            $('#ModalVisit').modal('show');
        }

        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });


            var gvClientDisst = $('#<%=this.gvClientDisst.ClientID %>');
            gvClientDisst.Scrollable();
        }

    </script>
 



    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">

                <fieldset class="scheduler-border fieldset_B">

                    <div class="form-horizontal">


                        <div class="form-group">
                            <div class="col-md-4 pading5px ">

                                <asp:Label ID="lblPage" runat="server" CssClass="l smLbl_to" Text="Page Size" Visible="False"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" ddlPage"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">

                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                </asp:DropDownList>

                                <asp:Label ID="lblfrmdate" runat="server" CssClass=" smLbl_to" Text="Month:"></asp:Label>

                                <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True"
                                    TabIndex="11" CssClass="ddlPage125 inputTxt">
                                </asp:DropDownList>



                                <div class="colMdbtn pading5px">
                                    <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                                </div>
                            </div>



                            <div class="col-md-1">

                                <asp:CheckBox ID="chktwise" runat="server" TabIndex="10" Text="Team Wise" CssClass="btn btn-primary checkBox" />


                            </div>
                            <div class="col-md-2 pading5px asitCol3">
                                <div class="msgHandSt">
                                    <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                </div>

                            </div>



                        </div>
                    </div>
                </fieldset>

                
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
                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "mteamdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "mteamdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                        
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "") 
                                                                         
                                                                    %>'
                                        Width="150px"
                                        Style="text-align: left" ForeColor="#155273" BackColor="Transparent" BorderStyle="None" Font-Size="13px" ></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="tamt1">
                                <ItemTemplate>
                                    <asp:Label ID="lbltamt1" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt1")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtamt1" runat="server" Style="text-align: right; color: black;" Width="60px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="tamt2">
                                <ItemTemplate>
                                    <asp:Label ID="lbltamt2" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt2")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtamt2" runat="server" Style="text-align: right; color: black;" Width="60px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="tamt3">
                                <ItemTemplate>
                                    <asp:Label ID="lbltamt3" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt3")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtamt3" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="tamt4">
                                <ItemTemplate>
                                    <asp:Label ID="lbltamt4" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt4")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtamt4" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="tamt5">
                                <ItemTemplate>
                                    <asp:Label ID="lbltamt5" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt5")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtamt5" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="tamt6">
                                <ItemTemplate>
                                    <asp:Label ID="lbltamt6" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt6")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtamt6" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="tamt7">
                                <ItemTemplate>
                                    <asp:Label ID="lbltamt7" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt7")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtamt7" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="tamt8">
                                <ItemTemplate>
                                    <asp:Label ID="lbltamt8" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt8")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtamt8" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="----">

                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="amt1">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblamt1" runat="server"
                                        Text='<%# "<B>" + Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt1")).ToString("#,##0;(#,##0); ") +"</B>" %>'
                                        Width="60px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px" OnClick="btnAmt1_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFamt1" runat="server" Style="text-align: right; color: black;" Width="60px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="amt2">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblamt2" runat="server"
                                        Text='<%# "<B>" + Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt2")).ToString("#,##0;(#,##0); ") +"</B>" %>'
                                        Width="60px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px" OnClick="btnAmt2_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFamt2" runat="server" Style="text-align: right; color: black;" Width="60px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="amt3">
                                <ItemTemplate>

                                    <asp:LinkButton ID="lblamt3" runat="server"
                                        Text='<%# "<B>" + Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt3")).ToString("#,##0;(#,##0); ") +"</B>" %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px" OnClick="btnAmt3_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFamt3" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="amt4">
                                <ItemTemplate>

                                    <asp:LinkButton ID="lblamt4" runat="server"
                                        Text='<%# "<B>" + Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt4")).ToString("#,##0;(#,##0); ") +"</B>" %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px" OnClick="btnAmt4_Click"></asp:LinkButton>
                                </ItemTemplate>



                                <FooterTemplate>
                                    <asp:Label ID="lblFamt4" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                             <asp:TemplateField HeaderText="amt5">
                                <ItemTemplate>

                                    <asp:LinkButton ID="lblamt5" runat="server"
                                        Text='<%# "<B>" + Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt5")).ToString("#,##0;(#,##0); ") +"</B>" %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px" OnClick="lblamt5_Click"></asp:LinkButton>
                                </ItemTemplate>



                                <FooterTemplate>
                                    <asp:Label ID="lblFamt5" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                             <asp:TemplateField HeaderText="amt6">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblamt6" runat="server"
                                        Text='<%# "<B>" + Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt6")).ToString("#,##0;(#,##0); ") +"</B>" %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px" OnClick="lblamt6_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFamt6" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="amt7">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblamt7" runat="server"
                                        Text='<%# "<B>" + Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt7")).ToString("#,##0;(#,##0); ") +"</B>" %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px" OnClick="lblamt6_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFamt7" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="amt8">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblamt8" runat="server"
                                        Text='<%# "<B>" + Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt8")).ToString("#,##0;(#,##0); ") +"</B>" %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px" OnClick="lblamt6_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFamt8" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Result">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkbtnpar" runat="server" Target="_blank"
                                        Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tpar")).ToString("#,##0.00;(#,##0.00);")+"</B>" %>'
                                        Width="35px"
                                        Style="text-align: right" BackColor="Transparent" Font-Size="13px" BorderStyle="None"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Grade">

                                <ItemTemplate>
                                    <asp:LinkButton ID="btnGpa" runat="server"
                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem,"gpa")) %>'
                                        Width="90px"
                                        Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px" OnClick="btnGpa_Click"></asp:LinkButton>
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

              


                <div class="modal fade AsitModal" id="exampleModal3" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog row">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close btn-danger" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                                <h3 class="modal-title" id="myModalLabel">Employee's KPI Performance Sheet</h3>


                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">

                                    <ContentTemplate>

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvEmpIndEva" runat="server" AllowPaging="false" Width="200px"
                                                AutoGenerateColumns="False" ShowFooter="true"
                                                CssClass="table table-striped table-hover table-bordered grvContentarea">
                                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                    Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="10px" />
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
                                                            <asp:Label ID="lblgvEmpname" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"empname")) %>'
                                                                Width="150px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotal" runat="server" Style="text-align: right; color: black;" Width="150px">Total:</asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Activity">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvGrp" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"kpigrpdesc")) %>'
                                                                Width="80px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="KPI Index">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvKpiVal" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"stdkpival")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="55px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFKpival" runat="server" Style="text-align: right; color: black;" Width="55px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Target">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTarget" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"stdtarget")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="90px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTarget" runat="server" Style="text-align: right; color: black;" Width="55px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Actual">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActual" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"actual")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="90px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Actual Index">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMparcnt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"mparcnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="55px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFMparcnt" runat="server" Style="text-align: right; color: black;" Width="55px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <%--<asp:LinkButton ID="lnkClose" runat="server" CssClass="btn btn-default" OnClick="lnkClose_Click">Close</asp:LinkButton>--%>
                                <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="modal fade AsitModal" id="exampleModal4" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog row">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close btn-danger" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                                <h3 class="modal-title">Day Wise Employee's KPI Performance Sheet</h3>




                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">

                                    <ContentTemplate>

                                        <fieldset class="scheduler-border fieldset_B">

                                            <div class="form-horizontal">
                                                <div class="form-group">

                                                    <div class="col-md-4 ">
                                                        <asp:Label ID="lblName" runat="server" CssClass="lblTxt lblName220"></asp:Label>



                                                    </div>
                                                    <div class="col-md-4 ">
                                                        <asp:Label ID="lblDesg" runat="server" CssClass="lblTxt lblName220"></asp:Label>



                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblJoin" runat="server" TabIndex="4" CssClass="lblTxt lblName220"></asp:Label>



                                                    </div>




                                                </div>
                                        </fieldset>



                                        <div class="table-responsive">
                                            <asp:GridView ID="gvResMonth" runat="server" AllowPaging="false" Width="200px"
                                                AutoGenerateColumns="False" ShowFooter="true" OnRowDataBound="gvResMonth_RowDataBound"
                                                CssClass="table table-striped table-hover table-bordered grvContentarea">
                                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                    Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="10px" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="server"
                                                                Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem,"mondays"))+"</B>" %>'
                                                                Width="80px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFtotal" runat="server" Style="text-align: right; color: black;" Font-Bold="true" Width="80px">Total: </asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="tamt1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltamt1" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt1")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFtamt1" runat="server" Style="text-align: right; color: black;" Font-Bold="true" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="tamt2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltamt2" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt2")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFtamt2" runat="server" Style="text-align: right; color: black;" Font-Bold="true" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="tamt3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltamt3" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt3")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFtamt3" runat="server" Style="text-align: right; color: black;" Font-Bold="true" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="tamt4">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltamt4" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt4")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFtamt4" runat="server" Style="text-align: right; color: black;" Font-Bold="true" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="----">

                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblamt1" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt1")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFamt1" runat="server" Style="text-align: right; color: black;" Font-Bold="true" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblamt2" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt2")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFamt2" runat="server" Style="text-align: right; color: black;" Font-Bold="true" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblamt3" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt3")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFamt3" runat="server" Style="text-align: right; color: black;" Font-Bold="true" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt4">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblamt4" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt4")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFamt4" runat="server" Style="text-align: right; color: black;" Font-Bold="true" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <%--<asp:LinkButton ID="lnkClose" runat="server" CssClass="btn btn-default" OnClick="lnkClose_Click">Close</asp:LinkButton>--%>
                                <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal fade AsitModal" id="exampleModal5" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog row">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close btn-danger" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                                <h3 class="modal-title">Employee's KPI Performance Graph</h3>


                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">

                                    <ContentTemplate>

                                        <div class="table-responsive">
                                            <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Height="270px" BorderColor="Black" Visible="false">
                                                <asp:Chart ID="Chart1" runat="server" Height="264px" Width="830px">
                                                    <Series>
                                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                                            MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="5">
                                                        </asp:Series>
                                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                                            MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="5">
                                                        </asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea Name="ChartArea1">

                                                            <AxisX MaximumAutoSize="100" Interval="1" TitleFont="Sans Serif">
                                                            </AxisX>
                                                        </asp:ChartArea>
                                                    </ChartAreas>
                                                    <Titles>
                                                        <asp:Title Font="Cambria, 20px" ForeColor="Red" Name="Title1"
                                                            Text="Monthly KPI Target Vs. Achievement">
                                                        </asp:Title>
                                                    </Titles>
                                                </asp:Chart>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <%--<asp:LinkButton ID="lnkClose" runat="server" CssClass="btn btn-default" OnClick="lnkClose_Click">Close</asp:LinkButton>--%>
                                <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <%--  Discussion--%>

                <div class="modal fade AsitModal" id="exampleModal6" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog row">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close btn-danger" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                                <h3 class="modal-title">Client Discussion History</h3>




                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">

                                    <ContentTemplate>

                                        <%--<fieldset class="scheduler-border fieldset_B">

                                            <div class="form-horizontal">
                                                <div class="form-group">

                                                    <div class="col-md-4 ">
                                                        <asp:Label ID="Label1" runat="server"   CssClass="lblTxt lblName220"></asp:Label>

                                                        

                                                    </div>
                                                     <div class="col-md-4 ">
                                                        <asp:Label ID="Label2" runat="server"  CssClass="lblTxt lblName220"></asp:Label>

                                                        

                                                    </div>
                                                     <div class="col-md-4">
                                                        <asp:Label ID="Label3" runat="server"  TabIndex="4" CssClass="lblTxt lblName220"></asp:Label>

                                                       

                                                    </div>
                                                   
                                                    


                                                </div>
                                        </fieldset>--%>



                                        <div class="table-responsive">
                                            <asp:GridView ID="gvClientDis" runat="server" AllowPaging="False" Width="200px"
                                                AutoGenerateColumns="False" ShowFooter="true"
                                                CssClass="table table-striped table-hover table-bordered grvContentarea">
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
                                                    <asp:TemplateField HeaderText="Employee Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblgvEmpname" runat="server"
                                                                Text='<%# "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem,"empname"))+"</B>" %>'
                                                                Width="150px"
                                                                Style="text-align: left" ForeColor="#155273" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Client Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClient" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"prosdesc")) %>'
                                                                Width="120px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblphone" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"phone")) %>'
                                                                Width="80px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc")) %>'
                                                                Width="60px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt2" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc1")) %>'
                                                                Width="100px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="t3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt3" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc2")) %>'
                                                                Width="100px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t4">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt4" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc3")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t5">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt5" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc4")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t6">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt6" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc5")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t7">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt7" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc6")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t8">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt8" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc7")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t9">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt9" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc8")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt10" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc9")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t11">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt11" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc10")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t12">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt12" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc11")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t13">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt13" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc12")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t14">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt14" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc13")) %>'
                                                                Width="60px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t15">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblt15" runat="server" TextMode="MultiLine"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc14")) %>'
                                                                Width="120px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t16">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt16" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc15")) %>'
                                                                Width="60px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t17">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt17" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc16")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t18">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt18" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc17")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t19">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt19" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc18")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="t20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblt20" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc19")) %>'
                                                                Width="60px"
                                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <%--<asp:LinkButton ID="lnkClose" runat="server" CssClass="btn btn-default" OnClick="lnkClose_Click">Close</asp:LinkButton>--%>
                                <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                            </div>
                        </div>
                    </div>
                </div>




                <%--Sales--%>

                <div class="modal fade AsitModal" id="SalesModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog row">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close btn-danger" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                                <h3 class="modal-title">Day Wise Sales
                                <span class="input-group-btn" style="float: right; margin-top: 2px; margin-right: 60px;">
                                        <asp:LinkButton ID="lnkprintsales" runat="server" OnClick="lnkprintsales_Click" CssClass="btn btn-success printBtn okBtn"><span class="glyphicon glyphicon-print asitGlyp" style="color:white;" aria-hidden="true"></span> </asp:LinkButton>
                                    </span>
                                    <asp:DropDownList ID="DDPrintOptsales" runat="server" CssClass="form-control inputTxt" Style="width: 200px; float: right;">
                                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                                    </asp:DropDownList>

                                </h3>


                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">

                                    <ContentTemplate>

                                        <fieldset class="scheduler-border fieldset_B">

                                            <div class="form-horizontal">
                                                <div class="form-group">

                                                    <div class="col-md-4 ">
                                                        <asp:Label ID="lblNameS" runat="server" CssClass="lblTxt lblName220"></asp:Label>



                                                    </div>
                                                    <div class="col-md-4 ">
                                                        <asp:Label ID="lblDesgS" runat="server" CssClass="lblTxt lblName220"></asp:Label>



                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblJoinS" runat="server" TabIndex="4" CssClass="lblTxt lblName220"></asp:Label>



                                                    </div>




                                                </div>
                                        </fieldset>



                                        <div class="table-responsive">
                                            <asp:GridView ID="gvDayWSale" runat="server" AllowPaging="false" Width="200px"
                                                AutoGenerateColumns="False" ShowFooter="true"
                                                CssClass="table table-striped table-hover table-bordered grvContentarea">
                                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                    Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDPactdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDcuname" runat="server"
                                                                Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDResDesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="150px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgUnit" runat="server"
                                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "munit"))
                                                                         %>'
                                                                Width="35px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unit Size">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUSize" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="55px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Price per SFT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUpsft" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sftpr")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="55px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Budgeted Amt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDTAmt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="75px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFDTAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sold Amt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDSAmt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="75px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFDSAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Sold Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDSchdate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                                Width="65px" Style="text-align: left"></asp:Label>
                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Discount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDDisAmt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFDDisAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgDvDisPer" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="30px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <%--<asp:LinkButton ID="lnkClose" runat="server" CssClass="btn btn-default" OnClick="lnkClose_Click">Close</asp:LinkButton>--%>
                                <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <%--Collection--%>

                <div class="modal fade AsitModal" id="CollectionModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog row">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close btn-danger" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                                <h3 class="modal-title">Day Wise Collection
                                    <span class="input-group-btn" style="float: right; margin-top: 2px; margin-right: 60px;">
                                        <asp:LinkButton ID="lnkprintColl" runat="server" OnClick="lnkprintColl_Click" CssClass="btn btn-success printBtn okBtn"><span class="glyphicon glyphicon-print asitGlyp" style="color:white;" aria-hidden="true"></span> </asp:LinkButton>
                                    </span>
                                    <asp:DropDownList ID="DDPrintOptColl" runat="server" CssClass="form-control inputTxt" Style="width: 200px; float: right;">
                                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                                    </asp:DropDownList>



                                </h3>
                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">

                                    <ContentTemplate>

                                        <fieldset class="scheduler-border fieldset_B">

                                            <div class="form-horizontal">
                                                <div class="form-group">

                                                    <div class="col-md-4 ">
                                                        <asp:Label ID="lblNameC" runat="server" CssClass="lblTxt lblName220"></asp:Label>



                                                    </div>
                                                    <div class="col-md-4 ">
                                                        <asp:Label ID="lblDesgC" runat="server" CssClass="lblTxt lblName220"></asp:Label>



                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblJoinC" runat="server" TabIndex="4" CssClass="lblTxt lblName220"></asp:Label>



                                                    </div>




                                                </div>
                                        </fieldset>



                                        <div class="table-responsive">
                                            <asp:GridView ID="grvTrnDatWise" runat="server" AllowPaging="False"
                                                AutoGenerateColumns="False" ShowFooter="true"
                                                CssClass="table table-striped table-hover table-bordered grvContentarea">
                                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                    Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Group Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcGrpt" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MR No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcMRNo" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="MR Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcMRDat" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcProDesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="145px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUnDes" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Customer Name ">
                                                        <FooterTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFCDTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            ForeColor="Black" Style="text-align: right" Width="80px">Total:</asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFCDNetTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            ForeColor="Black" Style="text-align: right" Width="80px">Net Total:</asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCuName" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                                Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Cheque No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvChNo" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvBaNo" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvChDat" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqdate")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash Amt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCaAmt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFCashamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            ForeColor="Black" Style="text-align: right" Width="70px"> </asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque Amt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvChAmt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>


                                                        <FooterTemplate>

                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFChqamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvCDNetTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>


                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reconciliation Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvRecDat1" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Entry Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvEntrydate" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entrydat")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <%--<asp:LinkButton ID="lnkClose" runat="server" CssClass="btn btn-default" OnClick="lnkClose_Click">Close</asp:LinkButton>--%>
                                <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                            </div>
                        </div>
                    </div>
                </div>


                <%--Visit --%>




                <div class="modal fade AsitModal" id="ModalVisit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog row">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close btn-danger" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                                <h3 class="modal-title">Client Discussion History
                                    <%--<span class="pull-right">--%>


                                    <span class="input-group-btn" style="float: right; margin-top: 2px; margin-right: 60px;">
                                        <asp:LinkButton ID="lnkPrint2" runat="server" OnClick="lnkprint2_Click" CssClass="btn btn-success printBtn okBtn"><span class="glyphicon glyphicon-print asitGlyp" style="color:white;" aria-hidden="true"></span> </asp:LinkButton>
                                    </span>
                                    <asp:DropDownList ID="DDPrintOpt2" runat="server" CssClass="form-control inputTxt" Style="width: 200px; float: right;">
                                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                                    </asp:DropDownList>





                                    <%--</span>--%>
                                </h3>





                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always">

                                    <ContentTemplate>
                                        <div class="row">
                                            <asp:Label ID="lblgrpvisit" runat="server" CssClass=" smLbl_to" Text="A. Visit"></asp:Label>

                                        </div>

                                        <asp:GridView ID="gvClientDisst" runat="server" AllowPaging="False"
                                            AutoGenerateColumns="False" ShowFooter="true"
                                            CssClass="table-striped table-hover table-bordered grvContentarea">
                                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                Mode="NumericFirstLast" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNost" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1).Trim()+"." %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="28" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Client Name" HeaderStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClientst" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"prosdesc")) %>'
                                                            Width="120px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblphonest" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"phone")) %>'
                                                            Width="60px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Meeting Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltst1" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc")) %>'
                                                            Width="60px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltst2" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc1")) %>'
                                                            Width="100px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Unit & Size">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltst3" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"uausize")) %>'
                                                            Width="100px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Asking Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltst8" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc7")) %>'
                                                            Width="60px"
                                                            Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Offer Price" HeaderStyle-Width="60px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltst12" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc11")) %>'
                                                            Width="63px"
                                                            Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Booking Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltst13" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc12")) %>'
                                                            Width="60px"
                                                            Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="GAP %">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdfifinpercnt" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"difpercnt")).ToString("#,##0.00;(#,##0.00);") %>'
                                                            Width="50px"
                                                            Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Discusstion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltst15" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc14")) %>'
                                                            Width="260px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Appoinment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltst16" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc15")) %>'
                                                            Width="60px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>


                                        <div class="row">
                                            <asp:Label ID="lblgrpCall" runat="server" CssClass=" smLbl_to" Text="B.Offer, Call & Others "></asp:Label>

                                        </div>

                                        <asp:GridView ID="gvclientCall" runat="server" AllowPaging="False"
                                            AutoGenerateColumns="False" ShowFooter="true"
                                            CssClass="table-striped table-hover table-bordered grvContentarea">
                                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                Mode="NumericFirstLast" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlcall" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1).Trim()+"." %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="28" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Client Name" HeaderStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClientcall" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"prosdesc")) %>'
                                                            Width="120px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblphonecall" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"phone")) %>'
                                                            Width="60px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Meeting Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmdatecall" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc")) %>'
                                                            Width="60px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblprojectcall" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc1")) %>'
                                                            Width="100px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Unit & Size">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblunitcall" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"uausize")) %>'
                                                            Width="100px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Asking Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblaskpricecall" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc7")) %>'
                                                            Width="60px"
                                                            Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Offer Price" HeaderStyle-Width="60px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbloffpricecall" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc11")) %>'
                                                            Width="63px"
                                                            Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Booking Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbookamtcall" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc12")) %>'
                                                            Width="60px"
                                                            Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="GAP %">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdfifinpercntcall" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"difpercnt")).ToString("#,##0.00;(#,##0.00);") %>'
                                                            Width="50px"
                                                            Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Discusstion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldiscussion" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc14")) %>'
                                                            Width="260px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Appoinment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblappoinmentcall" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"gdesc15")) %>'
                                                            Width="60px"
                                                            Style="text-align: left" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <%--<asp:LinkButton ID="lnkClose" runat="server" CssClass="btn btn-default" OnClick="lnkClose_Click">Close</asp:LinkButton>--%>
                                <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                            </div>
                        </div>
                    </div>
                </div>




            </div>
        </div>
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

