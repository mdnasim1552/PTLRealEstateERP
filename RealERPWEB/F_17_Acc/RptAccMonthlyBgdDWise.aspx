<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccMonthlyBgdDWise.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccMonthlyBgdDWise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });

            $('#<%=this.dgv2.ClientID%>').Scrollable();
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
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="Label7" runat="server" CssClass="smLbl_to" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtCurDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate">
                                        </cc1:CalendarExtender>
                                        <asp:RadioButtonList ID="rbtnList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged"
                                            RepeatColumns="2" RepeatDirection="Horizontal" ForeColor="black" Width="160px">
                                            <asp:ListItem Selected="True">Department</asp:ListItem>
                                            <asp:ListItem>Accounts</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>
                                    <div class="col-md-3" id="Deptpanel" runat="server">
                                        <asp:Label ID="lblDept" runat="server" CssClass="smLbl_to" Text="Department"></asp:Label>
                                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="chzn-select inputTxt" Width="180px"
                                            TabIndex="6">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3" id="Accpanel" runat="server">
                                        <asp:Label ID="lblachead" runat="server" CssClass="smLbl_to" Text="Acc Head"></asp:Label>
                                        <asp:DropDownList ID="ddlActcode" AutoPostBack="true" OnSelectedIndexChanged="ddlActcode_SelectedIndexChanged" runat="server" CssClass="chzn-select inputTxt" Width="180px"
                                            TabIndex="6">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3" style="display: none;">
                                        <asp:Label ID="Label3" runat="server" CssClass="smLbl_to" Text="Resource"></asp:Label>
                                        <asp:DropDownList ID="ddlres" runat="server" CssClass="chzn-select inputTxt" Width="205px"
                                            TabIndex="6">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-md-3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Page Size" Visible="False"></asp:Label>
                                        <asp:DropDownList ID="droppagesixze" Visible="False" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                    </div>
                                </div>
                            </div>

                        </fieldset>


                        <div class="col-md-7">
                          
                                <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    RowStyle-Font-Size="12px" Width="400px" OnRowDataBound="dgv2_RowDataBound" ShowFooter="True">
                                    <PagerSettings Visible="False" />
                                    <RowStyle Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ActCode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepdesc1" runat="server"
                                                    Text='<%# "<B><span style=color:maroon>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gpdesc")) + "</span></B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "deptdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "gpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="150px">

                                                </asp:Label>
                                                <%--   <asp:Label ID="lblDepdesc1" runat="server"
                                                        Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                        Width="150px"></asp:Label>--%>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Head of Accounts">
                                            <FooterTemplate>
                                                <asp:Label ID="LblFoter" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccdesc" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="250px"></asp:Label>
                                                <%--    <asp:Label ID="lblAccdesc" runat="server" CssClass="GridLebelL"
                                                        Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="250px"></asp:Label>--%>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Department" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepdesc" runat="server"
                                                    Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budget">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvDrAmt" Font-Bold="true" runat="server" Style="background: none; border-style: none; text-align: right;" ReadOnly="True" Width="80px"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LblgvDrAmt" runat="server" MaxLength="15" Style="background: none; border-style: none; text-align: right;"
                                                    Text='<%# ((Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))=="Total") || (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))=="Grand Total"))?"<b>"+
                                                            Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ")+"</b>":Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />

                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Actual">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvActual" Font-Bold="true" runat="server" Style="background: none; border-style: none; text-align: right;" ReadOnly="True" Width="80px"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LblgvActualAmt" runat="server" MaxLength="15" Style="background: none; border-style: none; text-align: right;"
                                                    Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))=="Total")?"<b>"+
                                                            Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ")+"</b>":    Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />

                                        </asp:TemplateField>
                                    </Columns>

                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                    <AlternatingRowStyle />


                                </asp:GridView>
                          
                        </div>
                        <div class="col-md-5">
                            <asp:GridView ID="gvsummary" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                RowStyle-Font-Size="12px" ShowFooter="True">
                                <PagerSettings Visible="False" />
                                <RowStyle Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="Slblserialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ActCode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="SlblAccCod" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" Visible="false">
                                        <FooterTemplate>
                                            <asp:Label ID="SLblFoter2" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="SlblDepdesc1" runat="server"
                                                Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Head of Accounts">
                                        <FooterTemplate>
                                            <asp:Label ID="SLblFoter" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="SlblAccdesc" runat="server" CssClass="GridLebelL"
                                                Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Budget">
                                        <FooterTemplate>
                                            <asp:TextBox ID="StxtTgvDrAmt" Font-Bold="true" runat="server" Style="background: none; border-style: none; text-align: right;" ReadOnly="True" Width="80px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="SLblgvDrAmt" runat="server" MaxLength="15" Style="background: none; border-style: none; text-align: right;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />

                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Actual">
                                        <FooterTemplate>
                                            <asp:TextBox ID="StxtTgvActual" Font-Bold="true" runat="server" Style="background: none; border-style: none; text-align: right;" ReadOnly="True" Width="80px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="SLblgvActualAmt" runat="server" MaxLength="15" Style="background: none; border-style: none; text-align: right;"
                                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />

                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                                <AlternatingRowStyle />


                            </asp:GridView>
                        </div>



                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

