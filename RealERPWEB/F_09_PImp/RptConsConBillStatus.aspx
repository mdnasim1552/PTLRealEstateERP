
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptConsConBillStatus.aspx.cs" Inherits="RealERPWEB.F_09_PImp.RptConsConBillStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });


            var gv = $('#<%=this.gvbillstatus.ClientID %>');
            gv.Scrollable();

        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">



                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                               
                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label4" runat="server"
                                            Text="Project Name:"
                                            CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>


                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server"
                                            Width="300px" AutoPostBack="True" CssClass="chzn-select ddlistPull"
                                         >
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 asitCol1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server"
                                            OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>
                                    </div>

                                </div>
                                 <div class="form-group">
                                    <div class="col-md-12  pading5px">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName"
                                            Text="Date:"></asp:Label>

                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>


                                        <asp:Label ID="lbldateTo" runat="server" Font-Bold="True"
                                            Text="Date:" CssClass="smLbl_to" Visible="true"></asp:Label>

                                        <asp:TextBox ID="txttoDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                                        
                                        
                                        <asp:Label ID="lblReport" runat="server" CssClass="  smLbl_to"
                                            Text="Report"></asp:Label>
                                       
                                        <asp:DropDownList ID="ddlReport" runat="server"
                                            Width="156px" CssClass="ddlPage"
                                         >
                                          
                                            
                                            <asp:ListItem Value="ConSummary">Summary</asp:ListItem>
                                            <asp:ListItem Value="ConBill">Bill</asp:ListItem>
                                            <asp:ListItem Value="ConPayment">Payment</asp:ListItem>
                                          
                                            
                                        </asp:DropDownList>
                                
                                    </div>


                                </div>


                            </div>
                        </fieldset>


                        <asp:MultiView ID="MultiView1" runat="server">
                              <asp:View ID="ViewConBilsum" runat="server">
                                  
                                    <asp:GridView ID="gvconbillsum" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" AllowPaging="false">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl.No."><ItemTemplate><asp:Label ID="lblgvSlNocsum" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>




                                        <asp:TemplateField HeaderText="Sub-Contractor Name"><ItemTemplate><asp:Label ID="lblgvconnamecsum" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                    Width="130px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFTotalcsum" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="120px">Total :</asp:Label></FooterTemplate><FooterStyle HorizontalAlign="left" /><ItemStyle HorizontalAlign="left" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>


                                        <asp:TemplateField HeaderText="Bill Amount"><ItemTemplate><asp:Label ID="lblgvbillamtcsum" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvbillamtcsum" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        
                                        
                                          <asp:TemplateField HeaderText="Bill Finalization"><ItemTemplate><asp:Label ID="lblgvbillaamtcsum" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billaamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvbillaamtcsum" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>

                                        
                                        
                                        <asp:TemplateField HeaderText="Deduction"><ItemTemplate><asp:Label ID="lblgvdedamtcsum" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvdedamtcsum" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Penalty"><ItemTemplate><asp:Label ID="lblgvpenamtcsum" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "penamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvpenamtcsum" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        
                                         <asp:TemplateField HeaderText="Advanced"><ItemTemplate><asp:Label ID="lblgvadvamtcsum" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvadvamtcsum" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        
                                        
                                         <asp:TemplateField HeaderText="Security"><ItemTemplate><asp:Label ID="lblgvsecamtcsum" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sdamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvsecamtcsum" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Tax"><ItemTemplate><asp:Label ID="lblgvtaxamtcsum" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taxamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvtaxamtcsum" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Vat"><ItemTemplate><asp:Label ID="lblgvvatamtcsum" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vatamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvvatamtcsum" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Net Amount"><ItemTemplate><asp:Label ID="lblgvnetamtcsum" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvnetamtcsum" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        
                                        
                                          <asp:TemplateField HeaderText="Payment Amount"><ItemTemplate><asp:Label ID="lblgvpayamtcsum" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvpayamtcsum" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>

                                        

                                       
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                  
                                  </asp:View>

                            <asp:View ID="View1ConBIll" runat="server">
                                <asp:GridView ID="gvbillstatus" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" AllowPaging="false" OnPageIndexChanging="gvbillstatus_PageIndexChanging" OnRowDataBound="gvbillstatus_RowDataBound">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl.No."><ItemTemplate><asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>




                                        <asp:TemplateField HeaderText="Sub-Contractor Name"><ItemTemplate><asp:Label ID="lblgvconname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                    Width="130px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFTotal" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="120px">Total :</asp:Label></FooterTemplate><FooterStyle HorizontalAlign="left" /><ItemStyle HorizontalAlign="left" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        
                                         <asp:TemplateField HeaderText="Project"><ItemTemplate><asp:Label ID="lblgvproname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="130px"></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="left" /><ItemStyle HorizontalAlign="left" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        

                                        
                                          <asp:TemplateField HeaderText="Bill No"><ItemTemplate><asp:HyperLink ID="hlnkgvbillno" runat="server" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisuno1")) %>'
                                                    Width="60px"></asp:HyperLink></ItemTemplate><FooterStyle HorizontalAlign="left" /><ItemStyle HorizontalAlign="left" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        
                                          <asp:TemplateField HeaderText="Bill Date"><ItemTemplate><asp:Label ID="lblgvbilldate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isudat")) %>'
                                                    Width="65px"></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="left" /><ItemStyle HorizontalAlign="left" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>






                                        <asp:TemplateField HeaderText="Bill Amount"><ItemTemplate><asp:Label ID="lblgvbillamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:HyperLink ID="hlnkgvFgvbillamt" runat="server" Font-Bold="True" Target="_blank"
                                                    Style="text-align: right" Width="70px"></asp:HyperLink></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        
                                        
                                          <asp:TemplateField HeaderText="Bill Finalization"><ItemTemplate><asp:Label ID="lblgvbillaamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billaamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvbillaamt" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>

                                        
                                        
                                        <asp:TemplateField HeaderText="Deduction"><ItemTemplate><asp:Label ID="lblgvdedamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvdedamt" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Penalty"><ItemTemplate><asp:Label ID="lblgvpenamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "penamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvpenamt" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        
                                         <asp:TemplateField HeaderText="Advanced"><ItemTemplate><asp:Label ID="lblgvadvamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvadvamt" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        
                                        
                                         <asp:TemplateField HeaderText="Security"><ItemTemplate><asp:Label ID="lblgvsecamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sdamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvsecamt" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Tax"><ItemTemplate><asp:Label ID="lblgvtaxamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taxamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvtaxamt" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Vat"><ItemTemplate><asp:Label ID="lblgvvatamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vatamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvvatamt" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Net Amount"><ItemTemplate><asp:Label ID="lblgvnetamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvnetamt" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>


                                        

                                       
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>
                            
                            <asp:View ID="ViewConPayment" runat="server">
                                <asp:GridView ID="gvpayment" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvpayment_RowDataBound" >
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl.No."><ItemTemplate><asp:Label ID="lblgvSlNo0pay" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>




                                        <asp:TemplateField HeaderText="Sub-Contractor Name"><ItemTemplate><asp:Label ID="lblgvconanmepay" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                    Width="130px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFTotal" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="120px">Total :</asp:Label></FooterTemplate><FooterStyle HorizontalAlign="left" /><ItemStyle HorizontalAlign="left" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        
                                         <asp:TemplateField HeaderText="Project"><ItemTemplate><asp:Label ID="lblgvpronamepay" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="130px"></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="left" /><ItemStyle HorizontalAlign="left" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        

                                        
                                          <asp:TemplateField HeaderText="Voucher No"><ItemTemplate><asp:HyperLink ID="hlnkgvvounum" runat="server" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                    Width="65px"></asp:HyperLink></ItemTemplate><FooterStyle HorizontalAlign="left" /><ItemStyle HorizontalAlign="left" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        
                                          <asp:TemplateField HeaderText="Voucher Date"><ItemTemplate><asp:Label ID="lblgvvoudate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudate")) %>'
                                                    Width="65px"></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="left" /><ItemStyle HorizontalAlign="left" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>






                                        <asp:TemplateField HeaderText="Payment Amount"><ItemTemplate><asp:Label ID="lblgvpayamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFgvpayamt" runat="server" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="Bill Refno"><ItemTemplate><asp:Label ID="lblgvbillref" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                    Width="65px"></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="left" /><ItemStyle HorizontalAlign="left" /><HeaderStyle VerticalAlign="Top" /><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                         


                                        

                                       
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
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
            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



