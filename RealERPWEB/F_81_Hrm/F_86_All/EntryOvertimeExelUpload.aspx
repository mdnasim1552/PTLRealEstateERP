
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryOvertimeExelUpload.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_86_All.EntryOvertimeExelUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
        $('#<%=this.gvovertime.ClientID %>').tblScrollable();


            //gbattn.Scrollable();
           // $('.chzn-select').chosen({ search_contains: true });



        }

    </script>

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

            <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>

            <div class="row">
                <fieldset class="scheduler-border fieldset_A">
                    
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-12">
                                <asp:Label ID="lblPrevious" runat="server" CssClass=" smLbl_to" Text="Month:"></asp:Label>

                                <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True"
                                    TabIndex="11" CssClass=" ddlPage">
                                </asp:DropDownList>


                                   <div class=" form-group">
                                    <div class="col-sm-3">
                                        <asp:Panel ID="pnlxcel" runat="server">
                                            <asp:Label ID="lblExel" runat="server" CssClass="lblTxt lblName txtAlgRight" Text="Exele :"></asp:Label>
                                            <div class="uploadFile">
                                                <asp:FileUpload ID="fileuploadExcel" runat="server" onchange="submitform();" />
                                            </div>

                                        </asp:Panel>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="btnexcuplosd" runat="server" OnClick="btnexcuplosd_Click" BorderColor="White"
                                            CssClass="btn btn-primary okBtn" Text="Show"></asp:LinkButton>
                                    </div>

                                    <div class="clearfix"></div>
                                </div>
                                
                                
                            </div>



                           
                        </div>

                        

                        
            
                        </div>
                    
                </fieldset>
            </div>

         


            


            

            <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>


            <asp:GridView ID="gvovertime" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                AllowPaging="false">
                <RowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
               <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Card #">
                        <ItemTemplate>
                            <asp:Label ID="lblgvEmpIDCard" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                Width="40px"></asp:Label>
                        </ItemTemplate>

                        <FooterTemplate>
                            <asp:LinkButton ID="lFinalUpdatOvertime" runat="server" Font-Bold="True" CssClass="btn btn-danger primaryBtn" OnClick="lFinalUpdatOvertime_Click">Final Update</asp:LinkButton>

                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Fixed Hour">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvFixed" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Style="text-align: right"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fixedhour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="80px"></asp:TextBox>
                        </ItemTemplate>

                         <FooterTemplate>
                        <asp:Label ID="lgvFgvFixed" runat="server" Font-Bold="True" Font-Size="12px"
                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>

                  <%--  <asp:TemplateField HeaderText=" Hourly">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvhourly" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Style="text-align: right"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hourly")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="80px"></asp:TextBox>
                        </ItemTemplate>

                         <FooterTemplate>
                        <asp:Label ID="lgvFhourly" runat="server" Font-Bold="True" Font-Size="12px"
                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>--%>


                    




                </Columns>
                <FooterStyle CssClass="grvFooter" />
                <EditRowStyle />
                <AlternatingRowStyle />
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>


