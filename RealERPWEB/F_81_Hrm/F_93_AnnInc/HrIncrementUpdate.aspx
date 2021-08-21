<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="HrIncrementUpdate.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_93_AnnInc.HrIncrementUpdate" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };
    </script>

    
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3 ">
                                        <asp:Label ID="lblIncrementlist" runat="server" CssClass="lblTxt lblName">Increment List</asp:Label>
                                        <asp:TextBox ID="txtSrchIncrementList" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnIncrementList" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnIncrementList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlIncList" runat="server" CssClass="form-control inputTxt" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblddlIncrementNo" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-sm-1 pading5px">
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnShow_Click"
                                            TabIndex="3">Ok</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" Width="81px" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lbldate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txtdate">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-3 pading5px pull-right">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" ></asp:Label>
                                    </div>

                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvAnnIncre" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" Height="200px" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvAnnIncre_PageIndexChanging" ShowFooter="True"
                            Width="831px">
                            <PagerSettings Position="Top" />
                            <RowStyle/>
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server" Font-Bold="True"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSection" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name &amp; Designation">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkFiUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFiUpdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvndesig" runat="server"
                                            Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="160px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCardNo" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Joining Date">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvjoidat" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pre. Salary">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpreamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grossal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpresal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Increment %">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lgvincpercnt" runat="server" BackColor="Transparent"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inpercnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Increment Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lgvincamt" runat="server" BackColor="Transparent"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFincamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Final Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lgvfinamount" runat="server" BackColor="Transparent"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "finincamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFfinincamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Revise salary">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRevise" runat="server" BackColor="Transparent"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "revisesal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRevise" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Full Pay Bank/Cheque " Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkInc" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chksal"))=="True" %>'
                                            Width="30px" />
                                    </ItemTemplate>                                  
                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

