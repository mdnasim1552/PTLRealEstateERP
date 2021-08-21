<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptDetailsInfo.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptDetailsInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $('#<%=this.gvcondetails.ClientID %>').tblScrollable();
            $('#<%=this.gvPersonalInfo.ClientID %>').tblScrollable();
            $('.chzn-select').chosen({ search_contains: true });

        }

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
                        <fieldset class="scheduler-border fieldset_">
                            <div class="form-horizontal">

                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3" runat="server" Visible="False" id="supname">
                                        <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Text="Supplier" Width="70"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                     <div class="col-md-3 pading5px asitCol3" runat="server" Visible="False" id="conname">
                                        <asp:Label ID="lblconlist" runat="server" CssClass="lblTxt lblName" Text="Contractor" Width="70"></asp:Label>
                                        <asp:TextBox ID="txtconlist" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindCon" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>

                                    <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black"
                                        MaxDropDownHeight="250" TabIndex="8" TransitionalMode="true" Width="233px">
                                    </cc1:DropCheck>
                                    <div class="col-md-1" style="margin-left: 230px; margin-top: -20px;">

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>
                                    </div>
                                    <div class=" clearfix"></div>

                                    <div style="margin-left:380px; margin-top: -30px;"> 
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                    
                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                    
                        </fieldset>
                    </div>

                    <asp:MultiView ID="Multiview1" runat="server">
                        <asp:View runat="server" ID="vwPersonalInfo">
                            <div class="table-responsive">
                                <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="831px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supname")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Concern Person">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "conperson")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblphone" runat="server" BorderStyle="None" Width="100px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phoneno")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile">

                                            <ItemTemplate>
                                                <asp:Label ID="lblmobile" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Address">

                                            <ItemTemplate>
                                                <asp:Label ID="lbladdress" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supaddress")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Location">

                                            <ItemTemplate>
                                                <asp:Label ID="lbllocation" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Mail">

                                            <ItemTemplate>
                                                <asp:Label ID="lblmail" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mail")) %>'
                                                    Width="150px"></asp:Label>
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
                        </asp:View>
                        <asp:View runat="server" ID="vwConlist">
                            <div class="table-responsive">
                                <asp:GridView ID="gvcondetails" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="831px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo01" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid" runat="server" Font-Bold="True"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub. Cont Firm ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsubconname" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "conname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Proprietor">
                                            <ItemTemplate>
                                                <asp:Label ID="lblproprietor" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "conperson")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Contact No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmobile" runat="server" BorderStyle="None" Width="75px" style="word-break:break-all"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       

                                        <asp:TemplateField HeaderText="Permanent Address">

                                            <ItemTemplate>
                                                <asp:Label ID="lbladdress" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supaddress")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Type Of Work">

                                            <ItemTemplate>
                                                <asp:Label ID="lbllocation" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkt")) %>'
                                                    Width="120px"></asp:Label>
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
                        </asp:View>
                    </asp:MultiView>



                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


