<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptDeptEvaSheet.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.RptDeptEvaSheet" %>

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

        function pageLoaded() {
            $('#tblDeptEval').Scrollable({

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
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass=" smLbl_to" Text="Month:"></asp:Label>

                                        <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True"
                                            TabIndex="11" CssClass=" ddlPage" Style="width: 120px;">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px  asitCol3">

                                        <asp:Label ID="lblPage" runat="server" CssClass=" lblTxt lblName" Text="Page Size" Visible="False"></asp:Label>

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

                                    </div>

                                    






                                    <div class="col-md-1">
                                    </div>
                                    <div class="col-md-2 pading5px asitCol3">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                        </div>

                                    </div>



                                </div>
                            </div>
                        </fieldset>


                        <div class="table-responsive">

                            <asp:Repeater ID="reptDeptEval" runat="server" OnItemDataBound="reptDeptEval_ItemDataBound">

                                <HeaderTemplate>
                                    <table id="tblDeptEval" class=" table-striped table-hover table-bordered grvContentarea">
                                        <tr>
                                            <th>Sl</th>
                                            <th>Department Name</th>
                                              <th>Number of Emp.</th>
                                            <th>Month Target</th>
                                            <th>Month Actual</th>
                                            <th>Grade</th>

                                        </tr>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="10px"></asp:Label>

                                        </td>
                                        <%--<td>

                                            <asp:Label ID="lblgvDptid" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"deptcode")) %>'
                                                Width="200px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                        </td>--%>
                                        <td>
                                            <asp:HyperLink ID="hlnkempname" runat="server" Target="_blank"
                                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc"))%>' Style="text-align: left;width:150px;" ForeColor="#155273" BackColor="Transparent" BorderStyle="None" Font-Size="13px" ></asp:HyperLink>
                                        </td>
                                        <td style="text-align: right" >
                                            <asp:Label ID="lblempnumber" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"nofemp")).ToString("#,##0;(#,##0); ") %>'
                                               
                                                Style=" width:70px;" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </td>


                                        <td style="text-align: right">
                                            <asp:Label ID="lblgvtar" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tar")).ToString("#,##0.00;(#,##0.00); ") %>'
                                               
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblgvact" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"avgact")).ToString("#,##0.00;(#,##0.00); ") %>'
                                               
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="btnGpa" runat="server" Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem,"gpa"))+"</B>" %>'
                                                Width="100px" Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="13px" NavigateUrl="#">

                                            </asp:HyperLink>
                                           
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td></td>
                                        <td> </td>
                                        <td></td>
                                        <td> 
                                      </td>
                                        <td></td>
                                       

                                    </tr>
                                     </table>
                                </FooterTemplate>
                            </asp:Repeater>




                            <%--<asp:GridView ID="gvDptEval" runat="server" AllowPaging="True" Width="200px"
                                AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                CssClass="table table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvDptEval_RowDataBound" OnPageIndexChanging="gvDptEval_PageIndexChanging">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <ControlStyle Width="5px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Departmentcode" Visible="false">

                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Department Name">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>






                                    <asp:TemplateField HeaderText="Month Target">
                                        <ItemTemplate>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Month Actual">
                                        <ItemTemplate>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>






                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
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
                            </asp:GridView>--%>

                        </div>


                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

