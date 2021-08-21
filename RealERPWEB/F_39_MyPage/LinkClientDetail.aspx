<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkClientDetail.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.LinkClientDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard<a href="PurBillEntry.aspx">PurBillEntry.aspx</a>
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
           

        });
        function pageLoaded() {

            


          
            $('.chzn-select').chosen({ search_contains: true });
           

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
                                    <div class="col-md-8 pading5px ">
                                        <asp:Label ID="lblClientName" runat="server" CssClass="lblTxt lblName">Client Name</asp:Label>
                                           <asp:Label ID="lblclintnamedesc" runat="server" CssClass="inputlblVal"></asp:Label>
                                  </div>

                                    <div class="col-md-3 pading5px">

                                        <div class="msgHandSt">
                                            <asp:Label ID="lmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </fieldset>

                    </div>

                    <div class="row">

                       

                     

                         <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True"  
                                                OnRowDataBound="gvPersonalInfo_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"
                                                                ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                Width="90px" ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcResDesc1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                Width="150px" ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvgval" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Value">


                                                         <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdatPerInfo" runat="server"  CssClass="btn btn-danger primarygrdBtn"  onclick="lUpdatPerInfo_Click" >Update Personal Info</asp:LinkButton>
                                                </FooterTemplate>

                                                        <ItemTemplate>

                                                            <asp:TextBox ID="txtgvValMob" runat="server"  Visible="false"  TabIndex="30"  BackColor="Transparent" BorderStyle="None"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                Width="350px"></asp:TextBox>

                                                              <asp:TextBox ID="txtgvVal" runat="server"  BackColor="Transparent" BorderStyle="None"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                Width="350px"  TabIndex="31" ></asp:TextBox>
                                                                    <asp:DropDownList ID="ddlprofession"   TabIndex="32"  CssClass="form-control chzn-select" runat="server" Visible="false">
                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtgvCal" runat="server" Visible="false" TabIndex="33" BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>

                                                             <cc1:CalendarExtender ID="txtPublish_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtgvCal"></cc1:CalendarExtender>
                                                        </ItemTemplate>
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
                </div>
                <!-- End of contentpart-->

            </div>
            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





