<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkEmpEvaSheetGen.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.LinkEmpEvaSheetGen" %>

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
                                    <div class="col-md-4 pading5px">

                                        <asp:Label ID="lblPage" runat="server" CssClass=" lblTxt lblName" Text="Page Size"> </asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" >

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

                                        <asp:Label ID="lblymon" runat="server" CssClass=" smLbl_to" Text="Month:"></asp:Label>
                                        <asp:Label ID="lblvalymon" runat="server" CssClass=" inputlblVal" style=" width:90px;" ></asp:Label>

                                         <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                                        </div>
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
                                                Width="250px"
                                                Style="text-align: left" ForeColor="#155273" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:HyperLink>







                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Desgnation" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdesignation" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"desig")) %>'
                                                Width="150px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Month Target">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtar" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tar")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Month Actual">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvact" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"act")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
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



                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </div>


                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
